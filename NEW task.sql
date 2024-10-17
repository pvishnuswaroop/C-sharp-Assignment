

-- Step 1: Create the new database
CREATE DATABASE SISDB;
GO
USE SISDB;
GO

-- Step 2: Create the Students table
CREATE TABLE Students (
    student_id INT PRIMARY KEY IDENTITY(1,1),
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    date_of_birth DATE NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    phone_number VARCHAR(15) NOT NULL
);

-- Step 3: Create the Teacher table
CREATE TABLE Teacher (
    teacher_id INT PRIMARY KEY IDENTITY(1,1),
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL
);

-- Step 4: Create the Courses table
CREATE TABLE Courses (
    course_id INT PRIMARY KEY IDENTITY(1,1),
    course_name VARCHAR(100) NOT NULL,
    credits INT NOT NULL,
    teacher_id INT,
    FOREIGN KEY (teacher_id) REFERENCES Teacher(teacher_id)
);

-- Step 5: Create the Enrollments table
CREATE TABLE Enrollments (
    enrollment_id INT PRIMARY KEY IDENTITY(1,1),
    student_id INT NOT NULL,
    course_id INT NOT NULL,
    enrollment_date DATE NOT NULL,
    FOREIGN KEY (student_id) REFERENCES Students(student_id),
    FOREIGN KEY (course_id) REFERENCES Courses(course_id)
);

-- Step 6: Create the Payments table
CREATE TABLE Payments (
    payment_id INT PRIMARY KEY IDENTITY(1,1),
    student_id INT NOT NULL,
    amount DECIMAL(10, 2) NOT NULL,
    payment_date DATE NOT NULL,
    FOREIGN KEY (student_id) REFERENCES Students(student_id)
);


INSERT INTO Students (first_name, last_name, date_of_birth, email, phone_number)
VALUES
('Amit', 'Sharma', '1997-01-12', 'amit.sharma@example.in', '9123456789'),
('Neha', 'Patel', '1996-05-22', 'neha.patel@example.in', '9876543210'),
('Ravi', 'Verma', '1998-11-30', 'ravi.verma@example.in', '9988776655'),
('Priya', 'Singh', '1997-07-15', 'priya.singh@example.in', '9123344556'),
('Rahul', 'Kumar', '1995-09-05', 'rahul.kumar@example.in', '9234567890'),
('Sneha', 'Reddy', '1999-03-22', 'sneha.reddy@example.in', '9873456781'),
('Karan', 'Jain', '1996-11-15', 'karan.jain@example.in', '9834567123'),
('Nidhi', 'Kapoor', '1997-08-10', 'nidhi.kapoor@example.in', '9198765432'),
('Rohit', 'Mehra', '1998-04-05', 'rohit.mehra@example.in', '9876540987'),
('Arjun', 'Nair', '1996-09-14', 'arjun.nair@example.in', '9123456780'),
('John', 'Doe', '1995-08-15', 'john.doe@example.com', '123-456-7890'); 

INSERT INTO Teacher (first_name, last_name, email)
VALUES
('Ravi', 'Sharma', 'ravi.sharma@example.in'),
('Anjali', 'Patel', 'anjali.patel@example.in'),
('Sarah', 'Smith', 'sarah.smith@example.com'), 
('Pooja', 'Reddy', 'pooja.reddy@example.in'),
('Amit', 'Verma', 'amit.verma@example.in'),
('Suresh', 'Mehta', 'suresh.mehta@example.in'),
('Kavita', 'Desai', 'kavita.desai@example.in'),
('Rohan', 'Singh', 'rohan.singh@example.in'),
('Divya', 'Shah', 'divya.shah@example.in'),
('Vikas', 'Nair', 'vikas.nair@example.in');


INSERT INTO Courses (course_name, credits, teacher_id)
VALUES
('Introduction to Programming', 3, 1),  
('Mathematics 101', 3, 2),              
('Advanced Database Management', 4, 3), 
('Data Structures', 4, 4),              
('Operating Systems', 3, 5),            
('Computer Networks', 4, 6),            
('Cloud Computing', 3, 7),              
('Machine Learning', 4, 8),             
('Cyber Security', 3, 9),               
('Artificial Intelligence', 4, 10);     


INSERT INTO Enrollments (student_id, course_id, enrollment_date)
VALUES
(1, 1, '2023-01-10'), 
(2, 2, '2023-01-12'), 
(3, 3, '2023-01-15'), 
(4, 4, '2023-01-17'), 
(5, 5, '2023-01-18'), 
(6, 6, '2023-01-20'), 
(7, 7, '2023-01-22'), 
(8, 8, '2023-01-23'), 
(9, 9, '2023-01-24'), 
(10, 10, '2023-01-25'),
(1, 1, '2023-02-01'), 
(2, 2, '2023-02-03'); 


INSERT INTO Payments (student_id, amount, payment_date)
VALUES
(1, 10000, '2023-02-15'), 
(2, 12000, '2023-02-16'), 
(3, 15000, '2023-02-17'), 
(4, 11000, '2023-02-18'), 
(5, 13000, '2023-02-19'), 
(6, 14000, '2023-02-20'), 
(7, 16000, '2023-02-21'), 
(8, 17000, '2023-02-22'), 
(9, 18000, '2023-02-23'), 
(10, 19000, '2023-02-24'), 
(11, 500, '2023-04-10'); 

SELECT * FROM Students;
SELECT * FROM Teacher;
SELECT * FROM Courses;
SELECT * FROM Enrollments;
SELECT * FROM Payments;


