namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'8b924295-5f16-4e4f-b77f-5cdc50cbee88', N'admin@vidly.com', 0, N'AMUqSVvHyzF/57IbywKiIq8tl57LeskhF9plZFgpLiaJ5suQJK7owKulUNz4sTwi/A==', N'a56b4016-3a36-48ec-a447-1357b6dc9629', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'b2e86433-177e-4cd6-a8d1-ac9a95923d97', N'guest@vidly.com', 0, N'AJQzIikkZdlOJ3pXo1UHhdThc2wHf9JmEzliYYfhX1BcMlhA510044iBbbAGNc6ygQ==', N'cc0ac9ff-0dbb-483f-b6c3-b47b0c7a2f6d', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'31c4b524-ce1d-4753-95e3-8e2fb223bfd2', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8b924295-5f16-4e4f-b77f-5cdc50cbee88', N'31c4b524-ce1d-4753-95e3-8e2fb223bfd2')


                ");
        }
        
        public override void Down()
        {
        }
    }
}
