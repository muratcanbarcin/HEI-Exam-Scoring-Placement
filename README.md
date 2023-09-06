### HEI-Exam-Scoring-Placement

C# program for a simple student selection and placement system with candidates, exam answer sheets, department preferences, and the assignments of the candidates to the departments.

##### Candidates:
The candidate information is stored in a text file, named candidates.txt, in the following format:

`no, name surname, diploma-grade, dept-choice1, dept-choice2, dept-choice3, answer1, answer2, answer3, …, answer25`

- The first three elements are the candidate’s personal information
- After that, three elements are the candidate’s department choices to where she/he wishes to enroll
- The last twenty-five elements are the exam answers of the candidate.
The number of candidates is dynamic (maximum 40).

##### Departments

The quota information related to the departments is read from a text file, named departments.txt, in the following format:
`dept-no, dept-name, quota
`
The number of departments is dynamic (maximum 10).
The maximum quota for any department is 8.

##### Answer Sheet
The correct answers for the exam are stored in the array, named key, as follows:

`char[] key = {'A','B','D','C','C','C','A','D','B','C','D','B','A','C','B','A','C','D','C','D','A','D','B','C','D'};
`

##### Grades
The answers of the candidates will be graded by the system. The program must print the scores of all candidates on the screen.

There are 25 questions in the exam. Each question has four options (A, B, C, and D). Every correct answer will be graded as 4 points, therefore, the maximum grade is 100. Empty answers will not affect the grading. The candidate will lose 3 points for each wrong answer.

##### Assignment

Candidates are required to get a minimum score of 40 on the exam for enrolling in an undergraduate program.

The candidates can be placed in a department list according to their choices and the available quotas. Each candidate

specifies at most three department choices to where she/he wishes to enroll.
If the grades of two or more candidates are equal, the candidate with a higher “school diploma grade” will be chosen as the first. If they are equal again, the program can assign any of them to the department.

The program print the final results of assignments on the screen.

**The code was written without using Array.Sort().**

###End
