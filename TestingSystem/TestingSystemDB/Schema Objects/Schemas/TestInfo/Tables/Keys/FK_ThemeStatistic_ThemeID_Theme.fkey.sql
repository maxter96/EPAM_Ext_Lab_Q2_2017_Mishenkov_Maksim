ALTER TABLE [TestInfo].[ThemeStatistic]
    ADD CONSTRAINT [FK_ThemeStatistic_ThemeID_Theme] FOREIGN KEY ([ThemeID]) REFERENCES [TestInfo].[Theme] ([ThemeID]) ON DELETE CASCADE ON UPDATE NO ACTION;