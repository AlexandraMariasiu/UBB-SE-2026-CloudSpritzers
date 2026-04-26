using AutoMapper;
using CloudSpritzers1.Src.Model.Faq;
using CloudSpritzers1.Src.Dto;
using CloudSpritzers1.Src.Repository.Interfaces;
using CloudSpritzers1.Src.Dto.MappingProfiles;
using NSubstitute;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudSpritzers1Tests.Src.Dto.MappingProfiles;
[TestClass]
public class FAQEntryMappingProfileTests
{
    private IMapper _mapper;
    private FAQEntry _frequentlyAskedQuestionsEntry;
    private FAQEntryDTO _frequentlyAskedQuestionsDataTransferObject;

    [TestInitialize]
    public void Setup()
    {
        var configuration = new MapperConfiguration(mapperConfiguration => mapperConfiguration.AddProfile<FAQEntryMappingProfile>());
        _mapper = configuration.CreateMapper();
        _frequentlyAskedQuestionsEntry = new FAQEntry(1, "What cars can I park here?", "Only Audis", FAQCategoryEnum.Parking, 1, 1, 0);
        _frequentlyAskedQuestionsDataTransferObject = new FAQEntryDTO(1, "What cars can I park here?", "Only Audis", FAQCategoryEnum.Parking, 1, 1, 0);
    }

    // Entry to Data Transfer Object

    [TestMethod]
    public void Map_FromEntryToDTO_MapsIdCorrectly()
    {
        var result = _mapper.Map<FAQEntryDTO>(_frequentlyAskedQuestionsEntry);

        Assert.AreEqual(_frequentlyAskedQuestionsDataTransferObject.Id, result.Id);
    }
    [TestMethod]
    public void Map_FromEntryToDTO_MapsQuestionCorrectly()
    {
        var result = _mapper.Map<FAQEntryDTO>(_frequentlyAskedQuestionsEntry);

        Assert.AreEqual(_frequentlyAskedQuestionsDataTransferObject.Question, result.Question);
    }

    [TestMethod]
    public void Map_FromEntryToDTO_MapsAnswerCorrectly()
    {
        var result = _mapper.Map<FAQEntryDTO>(_frequentlyAskedQuestionsEntry);

        Assert.AreEqual(_frequentlyAskedQuestionsDataTransferObject.Answer, result.Answer); 
    }

    [TestMethod]
    public void Map_FromEntryToDTO_MapsViewCountCorrectly() 
    {
        var result = _mapper.Map<FAQEntryDTO>(_frequentlyAskedQuestionsEntry);

        Assert.AreEqual(_frequentlyAskedQuestionsDataTransferObject.ViewCount, result.ViewCount); 
    }

    [TestMethod]
    public void Map_FromEntryToDTO_MapsHelpfulVotesCountCorrectly()
    {
        var result = _mapper.Map<FAQEntryDTO>(_frequentlyAskedQuestionsEntry);

        Assert.AreEqual(_frequentlyAskedQuestionsDataTransferObject.HelpfulVotesCount, result.HelpfulVotesCount);
    }

    [TestMethod]
    public void Map_FromEntryToDTO_MapsNotHelpfulVotesCountCorrectly()
    {
        var result = _mapper.Map<FAQEntryDTO>(_frequentlyAskedQuestionsEntry);

        Assert.AreEqual(_frequentlyAskedQuestionsDataTransferObject.NotHelpfulVotesCount, result.NotHelpfulVotesCount);
    }

    // Data Transfer Object to Entry

    [TestMethod]
    public void Map_FromDtoToEntry_MapsIdCorrectly()
    {
        var result = _mapper.Map<FAQEntry>(_frequentlyAskedQuestionsDataTransferObject);

        Assert.AreEqual(_frequentlyAskedQuestionsEntry.Id, result.Id);
    }

    [TestMethod]
    public void Map_FromDtoToEntry_MapsQuestionCorrectly()
    {
        var result = _mapper.Map<FAQEntry>(_frequentlyAskedQuestionsDataTransferObject);

        Assert.AreEqual(_frequentlyAskedQuestionsEntry.Question, result.Question);
    }

    [TestMethod]
    public void Map_FromDtoToEntry_MapsAnswerCorrectly()
    {
        var result = _mapper.Map<FAQEntry>(_frequentlyAskedQuestionsDataTransferObject);

        Assert.AreEqual(_frequentlyAskedQuestionsEntry.Answer, result.Answer);
    }

    [TestMethod]
    public void Map_FromDtoToEntry_MapsViewCountCorrectly()
    {
        var result = _mapper.Map<FAQEntry>(_frequentlyAskedQuestionsDataTransferObject);

        Assert.AreEqual(_frequentlyAskedQuestionsEntry.ViewCount, result.ViewCount);
    }

    [TestMethod]
    public void Map_FromDtoToEntry_MapsHelpfulVotesCountCorrectly()
    {
        var result = _mapper.Map<FAQEntry>(_frequentlyAskedQuestionsDataTransferObject);

        Assert.AreEqual(_frequentlyAskedQuestionsEntry.HelpfulVotesCount, result.HelpfulVotesCount);
    }

    [TestMethod]
    public void Map_FromDtoToEntry_MapsNotHelpfulVotesCountCorrectly()
    {
        var result = _mapper.Map<FAQEntry>(_frequentlyAskedQuestionsDataTransferObject);

        Assert.AreEqual(_frequentlyAskedQuestionsEntry.NotHelpfulVotesCount, result.NotHelpfulVotesCount);
    }
}
