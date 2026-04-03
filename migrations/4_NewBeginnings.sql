use CloudSpritzers
go

SET IDENTITY_INSERT [User] ON;
INSERT INTO [User] ([user_id], [name], [email]) VALUES 
(0, 'Carlos BOT', 'customer-support@cloudspritzers.com');
SET IDENTITY_INSERT [User] OFF;