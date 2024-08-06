create DATABASE libery;

create table admin
(
    id int IDENTITY(1,1),
    [name] VARCHAR(20),
    email nvarchar(30),
    [password] NVARCHAR(20)

    CONSTRAINT pk_id PRIMARY KEY(id)
)


create table books
(
    id int identity(1,1),
    book_id as 'BOOK' + RIGHT('000'+CAST(id as VARCHAR(10)),3)PERSISTED,
    book_name VARCHAR(30),
    quantity INT

    CONSTRAINT pk_bookid PRIMARY KEY(id)
)

create table readers
(
    id int IDENTITY(1,1),
    red_name VARCHAR(30),
    email nvarchar(30),
    book_id int,
    out_date DATE,
    in_date date

    CONSTRAINT pk_readid PRIMARY KEY(id),
    CONSTRAINT fk_bookid FOREIGN KEY(book_id) REFERENCES books(id)
)


insert into admin
    ([name],email,[password])
VALUES
    ('nandha','nandha@gmail.com','nandha@123'),
    ('jega','jega@gmail.com','jega@123'),
    ('kanna','kanna@gmail.com','kanna@123');

INSERT into books   
    (book_name,quantity)
VALUES
    ('book1',5),
    ('book2',2),
    ('book3',3),
    ('book4',1),
    ('book5',5),
    ('book6',4),
    ('book7',6),
    ('book8',2),
    ('book9',1),
    ('book10',3);

INSERT INTO readers
    (red_name,email,book_id,out_date)
VALUES
    ('vivin','vivin@gmail.com',1,GETDATE());

truncate table readers;

create or alter PROCEDURE login_details @email NVARCHAR(30),@pass NVARCHAR(20)
AS
SELECT * from admin
WHERE email = @email AND [password]= @pass

Exec login_details @email='nandha@gmail.com',@pass = 'nandha@123';

SELECT * from books where book_name = 'book1';

SELECT * from admin;
SELECT * from books;
SELECT * from readers;

update books
set quantity = 5
where id =1;

create or alter function due_date (@date date)
returns DATE
as
BEGIN
    DECLARE @due date
    set @due = DATEADD (DAY,+7,@date)
    RETURN @due
END;

SELECT dbo.due_date(out_date) AS due_date
FROM readers
where id = 1;

update readers
set in_date = (SELECT dbo.due_date(out_date) AS due_date FROM readers where id = 1)
WHERE id = 1;

SELECT in_date from readers where id=1;


SELECT quantity from books where book_name = 'book1';


delete readers
WHERE ; 


update books
set quantity = 6
WHERE book_name = 'book1';

SELECT quantity from books where book_name = 'book1';

SELECT red_name , book_id from readers;


SELECT * from books;
SELECT * from readers;

SELECT book_name, red_name from books
join readers on books.id = readers.id;

create or alter PROCEDURE return_details @book_name NVARCHAR(30),@name NVARCHAR(20)
AS
SELECT book_name, red_name from books
join readers on books.id = readers.id
WHERE book_name = @book_name AND red_name= @name

Exec return_details @book_name='book2',@name = 'vivin';

create table new_user
(
    id int IDENTITY(1,1),
    user_id AS 'USER'+ RIGHT('000'+CAST(id as VARCHAR(10)),3)PERSISTED,
    name VARCHAR(20),
    age int,
    email NVARCHAR(30),
    [password] NVARCHAR(20)

    CONSTRAINT pk_user PRIMARY KEY (id)
)

insert into new_user
    (name,age,email,[password])
VALUES
    ('vivin',24,'vivin@gmail','vivin@123');

SELECT * from admin;
SELECT * from books;
SELECT * from readers;
SELECT * from new_user;