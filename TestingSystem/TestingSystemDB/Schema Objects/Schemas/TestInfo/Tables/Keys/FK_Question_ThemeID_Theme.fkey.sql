﻿ALTER TABLE [TestInfo].[Question]
    ADD CONSTRAINT [FK_Question_ThemeID_Theme] FOREIGN KEY ([ThemeID]) REFERENCES [TestInfo].[Theme] ([ThemeID]) ON DELETE CASCADE ON UPDATE NO ACTION;