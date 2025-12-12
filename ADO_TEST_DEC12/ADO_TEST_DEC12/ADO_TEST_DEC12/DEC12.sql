create database ADO_Test_dec12

create table students (
    studentid int primary key identity,
    fullname varchar(100) not null,
    email varchar(100) unique,
    department varchar(50) not null,
    yearofstudy int not null
);

insert into students (fullname, email, department, yearofstudy)
values
('Dharani sri', 'dharanisri.r@email.com', 'computer science and enginnering', 1),
('Harini sri', 'harinisri.r@email.com', 'information technology', 2),
('kesavika jaanu', 'kesavika.j@email.com', 'electrical and communication engineering', 3),
('Keerthana kannan', 'keerthana.k@email.com', 'computer science and engineering', 2),
('Sanjay kumar', 'sanjaykumar.na@email.com', 'artificial intelligence and data science', 4);


select * from students

create table courses (
    courseid int primary key identity,
    coursename varchar(100) not null,
    credits int not null,
    semester varchar(20) not null
);

insert into courses (coursename, credits, semester)
values
('database systems', 4, 'semester 1'),
('operating systems', 3, 'semester 2'),
('data structures', 4, 'semester 1'),
('computer networks', 3, 'semester 2'),
('software engineering', 3, 'semester 3'),
('machine learning', 4, 'semester 4');

select * from courses

create table enrollments (
    enrollmentid int primary key identity,
    studentid int foreign key references students(studentid),
    courseid int foreign key references courses(courseid),
    enrolldate datetime not null,
    grade varchar(5) null
);


insert into enrollments (studentid, courseid, enrolldate, grade)
values
(1, 1, getdate(), null),
(1, 3, getdate(), 'a'),
(2, 2, getdate(), 'b'),
(3, 4, getdate(), null),
(4, 1, getdate(), 'a'),
(5, 6, getdate(), 'c');



select * from enrollments


create procedure sp_GetCoursesBySemester
    @Semester VARCHAR(20)
as
begin
    select CourseId, CourseName, Credits
    from Courses
    where Semester = @Semester
end
