ALTER TABLE [TestInfo].[UserAnswer]
    ADD CONSTRAINT [FK_UserAnswer_AnswerID_Answer] FOREIGN KEY ([AnswerID]) REFERENCES [TestInfo].[Answer] ([AnswerID]) ON DELETE CASCADE ON UPDATE NO ACTION;