CREATE PROCEDURE [dbo].[usp_GetDashboardData]
AS
BEGIN
    SET NOCOUNT ON;

	CREATE TABLE #Counts (
        Metric NVARCHAR(255),
        Value INT
	);

    CREATE TABLE #RecentOrders (
        OrderID INT,
        CustomerName NVARCHAR(255),
        OrderDate DATETIME,
		PaymentMode VARCHAR(100)
    );

    CREATE TABLE #RecentProducts (
        ProductID INT,
        ProductName NVARCHAR(255),
	    ProductCode VARCHAR(100),
		Description VARCHAR(100)
    );

    CREATE TABLE #TopCustomers (
        CustomerName NVARCHAR(255),
        TotalOrders INT,
        Email NVARCHAR(255)
    );

    CREATE TABLE #TopSellingProducts (
        ProductName NVARCHAR(255),
	    TotalSoldQuantity INT,
		ProductCode VARCHAR(100)
    );

    ---- Step 1: Get Counts
    --
	INSERT INTO #Counts
        SELECT 'TotalCustomers', COUNT(*) FROM Customer
    INSERT INTO #Counts
	    SELECT 'TotalProducts', COUNT(*) FROM Product
	INSERT INTO #Counts
		SELECT 'TotalOrders',COUNT(*) FROM [Order]
	INSERT INTO #Counts
		SELECT 'TotalBills',COUNT(*) FROM Bills
	INSERT INTO #Counts
		SELECT 'TotalRevenue',SUM(NetAmount) FROM Bills
		
		
    -- Step 2: Get Recent 10 Orders
    INSERT INTO #RecentOrders
    SELECT TOP 10
        O.OrderID,
        C.CustomerName,
        O.OrderDate,
        O.PaymentMode
    FROM [Order] O
    INNER JOIN Customer C ON O.CustomerID = C.CustomerID
    ORDER BY O.OrderID DESC;

    -- Step 3: Get Recent 10 Newly Added Products
    INSERT INTO #RecentProducts
    SELECT TOP 10
        ProductID,
        ProductName,
        ProductCode,
        Description
    FROM Product
    ORDER BY ProductID DESC;

    -- Step 4: Get Top 10 Customers by Order Count
    INSERT INTO #TopCustomers
    SELECT TOP 10
        C.CustomerName,
        COUNT(O.OrderID) AS TotalOrders,
        C.Email
    FROM [Order] O
    INNER JOIN Customer C ON O.CustomerID = C.CustomerID
    GROUP BY C.CustomerName, C.Email
    ORDER BY COUNT(O.OrderID) DESC;

    -- Step 5: Get Top 10 Selling Products
    INSERT INTO #TopSellingProducts
    SELECT TOP 10
        P.ProductName,
        SUM(OD.Quantity) AS TotalSoldQuantity,
        P.ProductCode
    FROM OrderDetail OD
    INNER JOIN Product P ON OD.ProductID = P.ProductID
    GROUP BY P.ProductName, P.ProductCode
    ORDER BY SUM(OD.Quantity) DESC;

    -- Output Results
    -- Output Counts
    SELECT * FROM #Counts;

    -- Output Recent Orders
    SELECT * FROM #RecentOrders;

    -- Output Recent Products
    SELECT * FROM #RecentProducts;

    -- Output Top Customers
    SELECT * FROM #TopCustomers;

    -- Output Top Selling Products
    SELECT * FROM #TopSellingProducts;

    -- Cleanup Temporary Tables
    DROP TABLE #RecentOrders;
    DROP TABLE #RecentProducts;
    DROP TABLE #TopCustomers;
    DROP TABLE #TopSellingProducts;
END;