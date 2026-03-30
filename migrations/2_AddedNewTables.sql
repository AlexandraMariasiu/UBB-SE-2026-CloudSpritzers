go

drop table if exists FAQOption
drop table if exists FAQNode


create table FAQNode (
	node_id int identity(1,1),
	constraint PK_Node primary key (node_id),
	question_text nvarchar(255),
	is_final_answer bit
)

create table FAQOption (
	option_id int identity(1,1),
	constraint PK_Option primary key (option_id),
	node_id int,
	constraint FK_ParentNode foreign key (node_id) references FAQNode(node_id),
	[label] nvarchar(255),
	next_option_id int,
	constraint FK_NextNode foreign key (next_option_id) references FAQNode(node_id),
)