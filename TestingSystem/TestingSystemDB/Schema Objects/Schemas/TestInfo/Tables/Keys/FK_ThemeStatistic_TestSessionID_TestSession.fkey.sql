﻿ALTER TABLE [TestInfo].[ThemeStatistic]
    ADD CONSTRAINT [FK_ThemeStatistic_TestSessionID_TestSession] FOREIGN KEY ([TestSessionID]) REFERENCES [TestInfo].[TestSession] ([TestSessionID]) ON DELETE CASCADE ON UPDATE NO ACTION;