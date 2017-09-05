ALTER TABLE [TestInfo].[Answer]
    ADD CONSTRAINT [FK_Answer_QuestionID_Question] FOREIGN KEY ([QuestionID]) REFERENCES [TestInfo].[Question] ([QuestionID]) ON DELETE CASCADE ON UPDATE NO ACTION;