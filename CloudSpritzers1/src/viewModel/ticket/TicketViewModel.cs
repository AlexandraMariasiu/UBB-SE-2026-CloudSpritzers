using AutoMapper;
using CloudSpritzers1.src.dto;
using CloudSpritzers1.src.model;
using CloudSpritzers1.src.model.ticket;
using CloudSpritzers1.src.repository.database;
using CloudSpritzers1.src.service;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudSpritzers1.src.service.interfaces;
using CloudSpritzers1.src.dto;
using CloudSpritzers1.src.model.ticket;
using CloudSpritzers1.src.service;
using AutoMapper;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CloudSpritzers1.src.viewModel
{
    public partial class TicketsViewModel : ObservableObject
    {
        private readonly ITicketService _ticketService;
        private readonly IMapper _mapper;
        private readonly ITicketCategoryService _categoryService;
        private readonly ITicketSubcategoryService _subcategoryService;
        private readonly IUserService _userService;

        public ObservableCollection<TicketDTO> AllTickets { get; } = new();

        private ObservableCollection<TicketDTO> _filteredTicketsForDisplay = new();
        public ObservableCollection<TicketDTO> FilteredTicketsForDisplay => _filteredTicketsForDisplay;

        private TicketFilterStatusEnum _selectedFilter = TicketFilterStatusEnum.ALL;

        public ObservableCollection<TicketCategory> Categories { get; } = new();
        public ObservableCollection<TicketSubcategory> Subcategories { get; } = new();

        [ObservableProperty]
        private string _newTicketTitle  = string.Empty;

        [ObservableProperty]
        private string _newTicketDescription = string.Empty;

        [ObservableProperty]
        private TicketCategory? _selectedCategory;

        [ObservableProperty]
        private TicketSubcategory? _selectedSubcategory;

        public TicketsViewModel(ITicketService ticketService, ITicketCategoryService categoryService, ITicketSubcategoryService subcategoryService, IUserService userService, IMapper mapper)
        {
            _ticketService = ticketService;
            _categoryService = categoryService;
            _subcategoryService = subcategoryService;
            _userService = userService;
            _mapper = mapper;

            LoadTickets();
            LoadCategories();
        }

        // =================================
        // PUBLIC API (UNCHANGED)
        // =================================
        public IEnumerable<TicketDTO> GetAllTickets()
        {
            return AllTickets;
        }

        public int GetTotalTicketCount()
        {
            return AllTickets.Count;
        }

        public TicketFilterStatusEnum SelectedFilterStatus
        {
            get => _selectedFilter;
            set
            {
                if (_selectedFilter != value)
                {
                    _selectedFilter = value;
                    ApplyFilterLogic();
                }
            }
        }

        public string SelectedFilterString
        {
            get => SelectedFilterStatus.ToString();
            set
            {
                if (Enum.TryParse<TicketFilterStatusEnum>(value, out var filter))
                    SelectedFilterStatus = filter;
            }
        }



        // =================================
        // LOAD FROM DATABASE
        // =================================
        private void LoadTickets()
        {
            var ticketsFromDatabase = _ticketService.GetAllTickets();

            AllTickets.Clear();

            foreach (var ticketEntity in ticketsFromDatabase)
            {
                var ticketDTO = _mapper.Map<TicketDTO>(ticketEntity);
                AllTickets.Add(ticketDTO);
            }

            ApplyFilterLogic();
        }

        // =================================
        // FILTER
        // =================================
        private void ApplyFilterLogic()
        {
            _filteredTicketsForDisplay.Clear();

            IEnumerable<TicketDTO> filteredResults = _ticketService.FilterTicketsByStatus(
                AllTickets ,
                SelectedFilterStatus
            );

            foreach (var ticket in filteredResults)
            {
                _filteredTicketsForDisplay.Add(ticket);
            }
        }

        // =================================
        // UPDATE STATUS
        // =================================
        public void UpdateStatus(int ticketId, TicketStatusEnum newStatus)
        {
            _ticketService.UpdateStatus(ticketId, newStatus);
            LoadTickets();
        }

        // =================================
        // UPDATE URGENCY
        // =================================
        public void UpdateUrgencyLevel(int ticketId, TicketUrgencyLevelEnum newUrgencyLevel)
        {
            _ticketService.UpdateUrgencyLevel(ticketId, newUrgencyLevel);
            LoadTickets();
        }

        // =================================
        // CREATE TICKET
        // =================================

        public void CreateTicket(TicketDTO ticketDTO)
        {
            // Fetch related entities from DB
            var creator = _userService.GetById(ticketDTO.CreatorAccountId);
            var category = _categoryService.GetCategoryById(ticketDTO.CategoryId);
            var subcategory = _subcategoryService.GetSubcategoryById(ticketDTO.SubcategoryId);

            var ticket = new Ticket(
                ticketDTO.TicketId,
                creator,
                ticketDTO.CurrentStatus,
                category,
                subcategory,
                ticketDTO.Subject,
                ticketDTO.Description,
                ticketDTO.CreationTimestamp,
                ticketDTO.UrgencyLevel
            );

            _ticketService.AddTicket(ticket);
            LoadTickets();
        }

        private void LoadCategories()
        {
            Categories.Clear();
            foreach (var categoryEntity in _categoryService.GetAllCategories())
                Categories.Add(categoryEntity);
        }

        public void LoadSubcategories(int categoryId)
        {
            Subcategories.Clear();
            foreach (var subcategoryEntity in _subcategoryService.GetSubcategoriesByCategoryId(categoryId))
                Subcategories.Add(subcategoryEntity);

        }

        public async Task SubmitNewTicketAsync()
        {
            // 3. Use the public generated properties (Capitalized)
            if (string.IsNullOrWhiteSpace(NewTicketTitle) || string.IsNullOrWhiteSpace(NewTicketDescription))
                throw new Exception("Please fill all required fields.");

            var newTicket = new TicketDTO(
                TicketId: GetTotalTicketCount() + 1,
                CreatorAccountId: 1,
                CreatorEmailAddress: "email@email.com",
                UrgencyLevel: TicketUrgencyLevelEnum.LOW,
                CurrentStatus: TicketStatusEnum.OPEN,
                CategoryId: SelectedCategory?.CategoryId ?? 1,
                CategoryName: SelectedCategory?.CategoryName ?? "General",
                SubcategoryId: SelectedSubcategory?.SubcategoryId ?? 1,
                SubcategoryName: SelectedSubcategory?.SubcategoryName ?? "General",
                Subject: NewTicketTitle,
                Description: NewTicketDescription,
                CreationTimestamp: DateTime.Now
            );

            CreateTicket(newTicket);

            // Reset fields
            NewTicketTitle = string.Empty;
            NewTicketDescription = string.Empty;
            SelectedCategory = null;
            SelectedSubcategory = null;
        }

        partial void OnSelectedCategoryChanged(TicketCategory? value)
        {
            if (value != null)
            {
                LoadSubcategories(value.CategoryId);
            }
            else
            {
                Subcategories.Clear();
            }
        }

    }

    public enum TicketFilterStatusEnum
    {
        ALL,
        OPEN,
        IN_PROGRESS,
        RESOLVED
    }



}