﻿ALTER TABLE [UserInfo].[UserInRole]
    ADD CONSTRAINT [FK_UserInRole_Role] FOREIGN KEY ([RoleID]) REFERENCES [UserInfo].[Role] ([RoleID]) ON DELETE CASCADE ON UPDATE NO ACTION;