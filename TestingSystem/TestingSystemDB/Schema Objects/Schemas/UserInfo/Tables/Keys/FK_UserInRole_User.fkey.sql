﻿ALTER TABLE [UserInfo].[UserInRole]
    ADD CONSTRAINT [FK_UserInRole_User] FOREIGN KEY ([UserID]) REFERENCES [UserInfo].[User] ([UserID]) ON DELETE CASCADE ON UPDATE NO ACTION;