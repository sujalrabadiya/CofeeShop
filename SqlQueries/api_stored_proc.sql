--City--
--City Select All--
CREATE PROCEDURE [dbo].[PR_LOC_City_SelectAll]
AS 
SELECT
		[dbo].[City].[CityID],
		[dbo].[City].[StateID],
		[dbo].[Country].CountryID,
		[dbo].[City].[CreatedDate],
		[dbo].[City].[ModifiedDate],
		[dbo].[City].[CityName],
		[dbo].[City].[CityCode]
		
FROM [dbo].[City]
LEFT OUTER JOIN [dbo].[State]
ON [dbo].[State].[StateID] = [dbo].[City].[StateID]
LEFT OUTER JOIN [dbo].[Country]
ON [dbo].[Country].[CountryID] = [dbo].[State].[CountryID]


--City Select by Id--
CREATE PROCEDURE [dbo].[PR_LOC_City_SelectByPK]
    @CityID INT
AS
BEGIN
    SELECT CityID, CityName, StateID, CountryID, CityCode
    FROM City
    WHERE CityID = @CityID
END

--City Insert--
CREATE PROCEDURE PR_LOC_City_Insert
    @CityName NVARCHAR(100),
    @CityCode NVARCHAR(10),
    @StateID INT,
    @CountryID INT
AS
BEGIN
    INSERT INTO City (CityName, CityCode, StateID, CountryID, CreatedDate)
    VALUES (@CityName, @CityCode, @StateID, @CountryID, GETDATE());
END

--City Update--
CREATE PROCEDURE PR_LOC_City_Update
    @CityID INT,
    @CityName NVARCHAR(100),
    @CityCode NVARCHAR(10),
    @StateID INT,
    @CountryID INT
AS
BEGIN
    UPDATE City
    SET CityName = @CityName,
        CityCode = @CityCode,
        StateID = @StateID,
        CountryID = @CountryID,
        ModifiedDate = GETDATE()
    WHERE CityID = @CityID;
END

--City Delete--
CREATE PROCEDURE PR_LOC_City_Delete
    @CityID INT
AS
BEGIN
    DELETE FROM City
    WHERE CityID = @CityID
END

--States by CountryId--
CREATE PROCEDURE [dbo].[PR_LOC_State_SelectComboBoxByCountryID]
@CountryID INT
AS 
SELECT
    [dbo].[State].[StateID],
    [dbo].[State].[StateName]
FROM [dbo].[State]
WHERE [dbo].[State].[CountryID] = @CountryID

--Country Select All--
CREATE PROCEDURE [dbo].[PR_LOC_Country_SelectComboBox]
AS 
SELECT
    COUNTRYID,
    COUNTRYNAME
FROM COUNTRY
ORDER BY COUNTRYNAME

-----State------
--Select All
CREATE PROCEDURE PR_LOC_State_SelectAll
AS
BEGIN
    SELECT
        StateID,
        StateName,
        StateCode,
        CountryID,
        CreatedDate,
        ModifiedDate
    FROM State
    ORDER BY StateName;
END

--Select by Id
CREATE PROCEDURE PR_LOC_State_SelectByPK
    @StateID INT
AS
BEGIN
    SELECT
        StateID,
        StateName,
        StateCode,
        CountryID,
        CreatedDate,
        ModifiedDate
    FROM State
    WHERE StateID = @StateID;
END

--Insert
CREATE PROCEDURE PR_LOC_State_Insert
    @StateName NVARCHAR(100),
    @StateCode NVARCHAR(10),
    @CountryID INT
AS
BEGIN
    INSERT INTO State (StateName, StateCode, CountryID, CreatedDate)
    VALUES (@StateName, @StateCode, @CountryID, GETDATE());
END

--Update
CREATE PROCEDURE PR_LOC_State_Update
    @StateID INT,
    @StateName NVARCHAR(100),
    @StateCode NVARCHAR(10),
    @CountryID INT
AS
BEGIN
    UPDATE State
    SET 
        StateName = @StateName,
        StateCode = @StateCode,
        CountryID = @CountryID,
        ModifiedDate = GETDATE()
    WHERE StateID = @StateID;
END

--Delete
CREATE PROCEDURE PR_LOC_State_Delete
    @StateID INT
AS
BEGIN
    DELETE FROM State
    WHERE StateID = @StateID;
END

-------Country---------
--Select All
CREATE PROCEDURE PR_LOC_Country_SelectAll
AS
BEGIN
    SELECT
        CountryID,
        CountryName,
        CountryCode,
        CreatedDate,
        ModifiedDate
    FROM Country
    ORDER BY CountryName;
END

--Select by Id
CREATE PROCEDURE PR_LOC_Country_SelectByPK
    @CountryID INT
AS
BEGIN
    SELECT
        CountryID,
        CountryName,
        CountryCode,
        CreatedDate,
        ModifiedDate
    FROM Country
    WHERE CountryID = @CountryID;
END

--Insert
CREATE PROCEDURE PR_LOC_Country_Insert
    @CountryName NVARCHAR(100),
    @CountryCode NVARCHAR(10)
AS
BEGIN
    INSERT INTO Country (CountryName, CountryCode, CreatedDate)
    VALUES (@CountryName, @CountryCode, GETDATE());
END

--Update
CREATE PROCEDURE PR_LOC_Country_Update
    @CountryID INT,
    @CountryName NVARCHAR(100),
    @CountryCode NVARCHAR(10)
AS
BEGIN
    UPDATE Country
    SET 
        CountryName = @CountryName,
        CountryCode = @CountryCode,
        ModifiedDate = GETDATE()
    WHERE CountryID = @CountryID;
END

--Delete
CREATE PROCEDURE PR_LOC_Country_Delete
    @CountryID INT
AS
BEGIN
    DELETE FROM Country
    WHERE CountryID = @CountryID;
END
