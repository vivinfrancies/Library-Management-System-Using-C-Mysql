CREATE TABLE Client
(
    [ID] INT IDENTITY,
    [ClientName] VARCHAR(30) NOT NULL,
    [ClientCompany] VARCHAR(30) NOT NULL,
    CONSTRAINT ClientPK PRIMARY KEY (ID)
)



CREATE TABLE Project (
    [ID] INT IDENTITY,
    [ProjectId] NVARCHAR(20) NOT NULL,
    [ProjectName] NVARCHAR(100) NOT NULL,
    [TaskName] VARCHAR(100) NOT NULL,
    [NoOfTask] INT,
    [CompletedTask] INT,
    [Description] NVARCHAR (120),
    [DueDate] Date,
    [ProjectCreatedDate] Date,
    [TeamLeadID] INT,
    [TeamMembersID] INT,
    [Progress] AS (100 / [NoOfTask] * [CompletedTask]),
    [ClientID] INT,
    [Status] VARCHAR (15),
    [FileUpload] NVARCHAR(MAX),
    [CompletedDate] DATE,
    [IsActive] INT,
    [CreatedDate] DATE,
    [ModifiedDate] DATE,
    [CreatedBy] VARCHAR(30),
    [ModifiedBy] VARCHAR(30),
    CONSTRAINT ProjectPK PRIMARY KEY (ID),
    CONSTRAINT Clientfk FOREIGN KEY (ClientID) REFERENCES Client(ID),
)

ALTER TABLE Project
ADD FOREIGN KEY (TeamMembersID)
REFERENCES ProjectMembers(ID)

ALTER TABLE Project
DROP CONSTRAINT Clientfk;

CREATE TABLE ProjectMembers(
    [ID] INT IDENTITY,
    [TeamMembersID] INT,
    [ProjectID] INT,

    CONSTRAINT ProjectMembersPK PRIMARY KEY (ID),
    CONSTRAINT ProjectIDfk FOREIGN KEY (ProjectID) REFERENCES Project(ID),
    CONSTRAINT TeamMembersIDfk FOREIGN KEY (TeamMembersID) REFERENCES EmployeeTable(ID)
)

CREATE TABLE Task(
    [ID] INT IDENTITY,
    [TaskName] NVARCHAR(60),
    [DueDate] DATE,
    [MembersID] INT,

    CONSTRAINT IDpk PRIMARY KEY(ID),
    CONSTRAINT MembersID FOREIGN KEY (MembersID) REFERENCES EmployeeTable(ID)
)


CREATE TABLE EmployeeTable(
    [ID] INT IDENTITY,
    [EmployeeId] VARCHAR(30),
    [FirstName] VARCHAR(40) NOT NULL,
    [MiddleName] VARCHAR(40) NOT NULL,
    [LastName] VARCHAR(20) NOT NULL,
    [Email] NVARCHAR(40) NOT NULL,
    [MobileNumber] VARCHAR(30) NOT NULL,
    [Address] NVARCHAR(150) NOT NULL,
    [City] VARCHAR(40) NOT NULL,
    [PinCode] VARCHAR(20) NOT NULL,
    [State] VARCHAR(40) NOT NULL,
    [Country] VARCHAR(30) NOT NULL,
    [Role] VARCHAR(30) NOT NULL,
    [Department] VARCHAR(40) NOT NULL,
    [Gender] VARCHAR(10) NOT NULL,
    [DateOfBirth] DATE NOT NULL,
    [DateOfJoining] DATE DEFAULT getdate(),
    [Religion] VARCHAR(30) NOT NULL,
    [Nationality] VARCHAR(30) NOT NULL,
    [MaritalStatus] VARCHAR(30) NOT NULL,
    [PgCollegeName] VARCHAR(30),
    [PgBatch] YEAR,
    [PgCourse] VARCHAR(30),
    [UgCollageName] VARCHAR(30),
    [UgBatch] YEAR,
    [UgCourse] VARCHAR(30),
    [DiplomaName] VARCHAR(30),
    [DiplomaBatch] YEAR,
    [DiplomaCourse] VARCHAR(30),
    [HSCName] VARCHAR(30),
    [HSCBatch] YEAR,
    [HSCCourse] VARCHAR(30),
    [SSLCName] VARCHAR(30) NOT NULL,
    [SSLCBatch] YEAR NOT NULL,
    [EmergencyContact] VARCHAR(10) NOT NULL,
    [Relationship] VARCHAR(30) NOT NULL,
    [ProfileImage] NVARCHAR(MAX) NOT NULL,
    [Salary] MONEY NOT NULL,
    [BankName] VARCHAR(50) NOT NULL,
    [AccountNumber] VARCHAR(20) NOT NULL,
    [IFSCCode] VARCHAR(30) NOT NULL,
    [IsActive] INT DEFAULT 1,
    [CreatedDate] DATE DEFAULT GETDATE(),
    [ModifiedDate] DATE DEFAULT GETDATE(),
    [CreatedName] VARCHAR(30),
    [ModifiedName] VARCHAR(30)
  )  

  CREATE TABLE ResignationTable(
    [ID] INT IDENTITY,
    [LastDateOfWorking] DATE NOT NULL,
    [ReasonForResign] VARCHAR(MAX) NOT NULL,
    [AdditionalReasonForResign] VARCHAR(MAX)
  )