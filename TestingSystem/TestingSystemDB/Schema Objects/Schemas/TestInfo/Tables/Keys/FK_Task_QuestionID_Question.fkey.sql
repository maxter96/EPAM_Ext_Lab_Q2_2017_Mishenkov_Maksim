ALTER TABLE [TestInfo].[Task]
    ADD CONSTRAINT [FK_Task_QuestionID_Question] FOREIGN KEY ([QuestionID]) REFERENCES [TestInfo].[Question] ([QuestionID]) ON DELETE CASCADE ON UPDATE NO ACTION;