/*
*
* A simple Customer schema
*
*/
CREATE TABLE Customers (
    Id INT IDENTITY(1, 1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    CreatedAt DATETIME2 DEFAULT SYSDATETIME()
);

/*
*
* A simple Merchant schema
*
*/
CREATE TABLE Merchants (
    Id INT IDENTITY(1, 1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    CreatedAt DATETIME2 DEFAULT SYSDATETIME()
);

/*
*
* A simple Transaction schema
*
*/
CREATE TABLE Transactions (
    Id INT IDENTITY(1, 1) PRIMARY KEY,
    CustomerId INT NOT NULL,
    MerchantId INT NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    CreatedAt DATETIME2 DEFAULT SYSDATETIME()
    FOREIGN KEY (CustomerId) REFERENCES Customers(Id),
    FOREIGN KEY (MerchantId) REFERENCES Merchants(Id)
);

/*
*
* A transactional log schema that gets added for every state (department)
* 
* Data column: A flexible schema to allow each departments handle their own data in the duration of the transaction
* Status column: A flexible schema to allow each department to check the status in the duration of the transaction
*
*/
CREATE TABLE TransactionLogs (
    Id INT IDENTITY(1, 1) PRIMARY KEY,
    TransactionId INT NOT NULL, 
    Data NVARCHAR(MAX) NOT NULL, -- Storing the data as JSON
    Status NVARCHAR(MAX) NOT NULL, -- Status of at this point of the transaction
    CreatedAt DATETIME2 DEFAULT SYSDATETIME(),
    CONSTRAINT CHK_Data CHECK (ISJSON(Data) > 0), -- Optional: Ensure TransactionData is valid JSON
    CONSTRAINT CHK_Status CHECK (ISJSON(Status) > 0) -- Optional: Ensure TransactionData is valid JSON
);