-- Script Date: 24/07/16 23:02  - ErikEJ.SqlCeScripting version 3.5.2.95
SELECT RoleName,Email,Password,UserName
From Roles
LEFT JOIN Users ON Roles.RoleId = Users.RoleId;