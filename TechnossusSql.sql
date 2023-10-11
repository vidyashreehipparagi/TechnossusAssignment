create table Student(
student_id int primary key identity(1,1),
student_name varchar(50),
fathers_name varchar(30),
mothers_name varchar(30),
age int,
home_address varchar(100),
registration_date datetime,
isActive int 
)
select * from Student
sp_help student
