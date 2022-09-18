use schoolBD

SET IDENTITY_INSERT Address ON

insert into Address (ID, Country, City, Street, PostalCode)
values (1, 'Ukraine', 'Malynivka', 'Kotsubinskogo, 2', 22360)

insert into Address (ID, Country, City, Street, PostalCode)
values (2, 'Ukraine', 'Lityn', 'Soborna, 25', 22300)

insert into Address (ID, Country, City, Street, PostalCode)
values (3, 'Ukraine', 'Vinnytsa', 'Privokzalna, 37', 22300)

SET IDENTITY_INSERT Address OFF

SET IDENTITY_INSERT School ON

insert into School (ID, Name, AddressID, OpeningDate)
values (1, 'Malynivka`s School', 1, '1991-01-01')

insert into School (ID, Name, AddressID, OpeningDate)
values (2, 'Lytin School 1', 2, '1992-02-04')

insert into School (ID, Name, AddressID, OpeningDate)
values (3, 'Random School', 3, '1973-05-09')

SET IDENTITY_INSERT School OFF

SET IDENTITY_INSERT Floor ON

insert into Floor (ID, Number, SchoolID)
values (1, 1, 1)

insert into Floor (ID, Number, SchoolID)
values (2, 2, 1)

insert into Floor (ID, Number, SchoolID)
values (3, 1, 2)

insert into Floor (ID, Number, SchoolID)
values (4, 2, 2)

insert into Floor (ID, Number, SchoolID)
values (5, 1, 3)

insert into Floor (ID, Number, SchoolID)
values (6, 2, 3)

insert into Floor (ID, Number, SchoolID)
values (7, 3, 3)

SET IDENTITY_INSERT Floor OFF

SET IDENTITY_INSERT RoomType ON

insert into RoomType (ID, Name)
values(1, 'Regular')

insert into RoomType (ID, Name)
values(2, 'Math')

insert into RoomType (ID, Name)
values(3, 'Biology')

insert into RoomType (ID, Name)
values(4, 'Geography')

insert into RoomType (ID, Name)
values(5, 'Literature')

insert into RoomType (ID, Name)
values(6, 'IT')

insert into RoomType (ID, Name)
values(7, 'Regular')

insert into RoomType (ID, Name)
values(8, 'Informatic')

insert into RoomType (ID, Name)
values(9, 'Gym')

insert into RoomType (ID, Name)
values(10, 'Hall')

insert into RoomType (ID, Name)
values(11, 'Workshop')

insert into RoomType (ID, Name)
values(12, 'Physics')

insert into RoomType (ID, Name)
values(13, 'Toilet')

insert into RoomType (ID, Name)
values(14, 'Citchen')

SET IDENTITY_INSERT RoomType OFF

SET IDENTITY_INSERT Room ON

insert into Room (ID, Number, FloorID, RoomTypeID)
values (1, 101, 1, 14)

insert into Room (ID, Number, FloorID, RoomTypeID)
values (2, 102, 1, 9)

insert into Room (ID, Number, FloorID, RoomTypeID)
values (3, 103, 1, 11)

insert into Room (ID, Number, FloorID, RoomTypeID)
values (4, 104, 1, 13)

insert into Room (ID, Number, FloorID, RoomTypeID)
values (5, 105, 1, 1)

insert into Room (ID, Number, FloorID, RoomTypeID)
values (6, 106, 1, 1)

insert into Room (ID, Number, FloorID, RoomTypeID)
values (7, 107, 1, 12)

insert into Room (ID, Number, FloorID, RoomTypeID)
values (8, 201, 2, 2)

insert into Room (ID, Number, FloorID, RoomTypeID)
values (9, 202, 2, 3)

insert into Room (ID, Number, FloorID, RoomTypeID)
values (10, 203, 2, 4)

insert into Room (ID, Number, FloorID, RoomTypeID)
values (11, 204, 2, 5)

insert into Room (ID, Number, FloorID, RoomTypeID)
values (12, 205, 2, 8)

insert into Room (ID, Number, FloorID, RoomTypeID)
values (13, 101, 3, 1)

insert into Room (ID, Number, FloorID, RoomTypeID)
values (14, 201, 4, 2)

insert into Room (ID, Number, FloorID, RoomTypeID)
values (15, 101, 5, 8)

insert into Room (ID, Number, FloorID, RoomTypeID)
values (16, 102, 5, 14)

insert into Room (ID, Number, FloorID, RoomTypeID)
values (17, 201, 6, 2)

insert into Room (ID, Number, FloorID, RoomTypeID)
values (18, 301, 7, 3)

SET IDENTITY_INSERT Room OFF

SET IDENTITY_INSERT Employee ON

insert into Employee (ID, FirstName, SecondName, Age)
values (1, 'Volodymir', 'Sychenko', 45)

insert into Employee (ID, FirstName, SecondName, Age)
values (2, 'Olena', 'Rogozha', 40)

SET IDENTITY_INSERT Employee OFF

insert into Director (ID)
values (1)

insert into Teacher (ID)
values (2)

insert into SchoolEmployee (EmployeeID, SchoolID)
values (1, 1)

insert into SchoolEmployee (EmployeeID, SchoolID)
values (2, 1)