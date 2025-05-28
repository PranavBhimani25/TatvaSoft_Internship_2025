
CREATE TABLE student (
  Firstname VARCHAR(255),
  Lastname VARCHAR(255),
  Enroll INT,
  Branch VARCHAR(50)
);
INSERT INTO student(Firstname, Lastname, Enroll, Branch)
VALUES
('Aman', 'Patel', 17, 'IOT'),
('Bhavin', 'Shah', 13, 'IT'),
('Chaitanya', 'Sharma', 19, 'CP'),
('Daksh', 'Patel', 16, 'CSD'),
('Harshit', 'Shah', 12, 'IOT'),
('Jay', 'Patil', 54, 'IT'),
('Kevin', 'Mishra', 44, 'CP'),
('Montu', 'Sharma', 18, 'CSD'),
('Naimish', 'Darji', 66, 'IOT'),
('Omkar', 'Patil', 74, 'IT'),
('Pinku', 'Shah', 56, 'CSD');

SELECT Firstname,Enroll FROM student;

SELECT * FROM student;



UPDATE student
SET Dept = CASE
    WHEN Branch IN ('IOT', 'IT') THEN 'Information Technology'
    WHEN Branch IN ('CP', 'CSD') THEN 'Computer Engineering'
    ELSE Dept
END;



DELETE FROM student WHERE enroll='16';
ALTER TABLE student RENAME COLUMN Branch TO Stream;

SELECT * FROM student WHERE Enroll=4;
SELECT * FROM student WHERE Enroll<30;
SELECT * FROM student WHERE Enroll<=30;
SELECT * FROM student WHERE Enroll>30;
SELECT * FROM student WHERE Enroll>=30;
SELECT * FROM student WHERE Enroll!=4;
SELECT * FROM student WHERE Firstname LIKE 'A%';
SELECT * FROM student WHERE Firstname LIKE 'Da%';
SELECT * FROM student WHERE Firstname ILIKE 'OM%';
SELECT * FROM student ORDER BY Enroll DESC;
SELECT * FROM student LIMIT 10;
SELECT * FROM student OFFSET 5 LIMIT 10; 


SELECT COUNT(*) FROM student;
SELECT AVG(Enroll) FROM student;
SELECT MAX(Enroll), MIN(Enroll) FROM student;
SELECT Stream, COUNT(*) FROM student GROUP BY Stream;
SELECT Stream, COUNT(*) FROM student GROUP BY Stream HAVING COUNT(*) > 2;
SELECT Stream, COUNT(*) FROM student GROUP BY Stream HAVING COUNT(*) < 3;

CREATE TABLE temporarytable(enrollment INT);
SELECT * FROM temporarytable;
DROP TABLE temporarytable;

DELETE FROM student
WHERE Firstname IS NULL
  AND Lastname IS NULL
  AND Enroll IS NULL
  AND Stream IS NULL;


ALTER TABLE student ADD COLUMN student_id SERIAL PRIMARY KEY;
ALTER TABLE student ALTER COLUMN Enroll SET NOT NULL;
ALTER TABLE student ADD Dept VARCHAR(255);
ALTER TABLE student
ADD temporarycolumn INT;
DROP COLUMN temporarycolumn;


CREATE TABLE dept (
  dept_id SERIAL PRIMARY KEY,
  dept_name VARCHAR(255) UNIQUE NOT NULL
);
INSERT INTO dept (dept_name)
VALUES
('Information Technology'),
('Computer Engineering');

ALTER TABLE student ADD COLUMN dept_id INT;

UPDATE student
SET dept_id = CASE
    WHEN Dept = 'Information Technology' THEN 1
    WHEN Dept = 'Computer Engineering' THEN 2
END;

ALTER TABLE student
ADD CONSTRAINT fk_dept
FOREIGN KEY (dept_id) REFERENCES dept(dept_id);

INSERT INTO dept (dept_name)
VALUES ('Data Science'); 

INSERT INTO student (Firstname, Lastname, Enroll, Stream, Dept, dept_id)
VALUES ('Jay', 'Patil', 54, 'None', NULL, NULL);

-- inner join
SELECT s.firstname, s.enroll, d.dept_name
FROM student s
INNER JOIN dept d ON s.dept_id = d.dept_id;

--left
SELECT s.firstname, s.enroll, d.dept_name
FROM student s
LEFT JOIN dept d ON s.dept_id = d.dept_id;

--right join
SELECT s.firstname, s.enroll, d.dept_name
FROM student s
RIGHT JOIN dept d ON s.dept_id = d.dept_id;


SELECT s.firstname, s.enroll, d.dept_name
FROM student s
FULL JOIN dept d ON s.dept_id = d.dept_id;
