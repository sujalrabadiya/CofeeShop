CREATE PROCEDURE [dbo].[PR_Customer_DropDown]
AS
BEGIN
    SELECT
		[dbo].[Customer].[CustomerID],
        [dbo].[Customer].[CustomerName]
    FROM
        [dbo].[Customer]
END

CREATE PROCEDURE [dbo].[PR_User_DropDown]
AS
BEGIN
    SELECT
		[dbo].[User].[UserID],
        [dbo].[User].[UserName]
    FROM
        [dbo].[User]
END

CREATE PROCEDURE [dbo].[PR_Product_DropDown]
AS
BEGIN
    SELECT
		[dbo].[Product].[ProductID],
        [dbo].[Product].[ProductName]
    FROM
        [dbo].[Product]
END

CREATE PROCEDURE [dbo].[PR_Order_DropDown]
AS
BEGIN
    SELECT
		[dbo].[Order].[OrderID],
        [dbo].[Order].[OrderDate]
    FROM
        [dbo].[Order]
END
