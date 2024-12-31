--1. Product Stored Procedures

--SelectAll Procedure
CREATE PROCEDURE [dbo].[PR_Product_SelectAll]
AS
    SELECT 
        P.ProductID, 
        P.ProductName, 
        P.ProductPrice, 
        P.ProductCode, 
        P.[Description], 
        U.UserName
    FROM Product P
    JOIN [User] U ON P.UserID = U.UserID
    ORDER BY P.ProductName

--SelectByPK Procedure
CREATE	 PROCEDURE [dbo].[PR_Product_SelectByPK]
@ProductID INT
AS
    SELECT 
        P.ProductID, 
        P.ProductName, 
        P.ProductPrice, 
        P.ProductCode, 
        P.[Description], 
        P.[UserID]
    FROM Product P
    WHERE P.ProductID = @ProductID
	
--Insert Procedure
CREATE PROCEDURE [dbo].[PR_Product_Insert]
@ProductName VARCHAR(100),
@ProductPrice DECIMAL(10,2),
@ProductCode VARCHAR(100),
@Description VARCHAR(100),
@UserID INT
AS
    INSERT INTO Product (ProductName, ProductPrice, ProductCode, [Description], UserID)
    VALUES (@ProductName, @ProductPrice, @ProductCode, @Description, @UserID)

--UpdateByPK Procedure
CREATE PROCEDURE [dbo].[PR_Product_UpdateByPK]
@ProductID INT,
@ProductName VARCHAR(100),
@ProductPrice DECIMAL(10,2),
@ProductCode VARCHAR(100),
@Description VARCHAR(100),
@UserID INT
AS
    UPDATE Product
    SET ProductName = @ProductName,
        ProductPrice = @ProductPrice,
        ProductCode = @ProductCode,
        [Description] = @Description,
        UserID = @UserID
    WHERE ProductID = @ProductID

--DeleteByPK Procedure
CREATE PROCEDURE [dbo].[PR_Product_DeleteByPK]
@ProductID INT
AS
    DELETE FROM Product
    WHERE ProductID = @ProductID

--2. User Stored Procedures

--SelectAll Procedure 
CREATE PROCEDURE [dbo].[PR_User_SelectAll]
AS
    SELECT UserID, UserName, Email, [Password], MobileNo, [Address], IsActive
    FROM [User]
    ORDER BY UserName

--SelectByPK Procedure
CREATE PROCEDURE [dbo].[PR_User_SelectByPK]
@UserID INT
AS
    SELECT UserID, UserName, Email, [Password], MobileNo, [Address], IsActive
    FROM [User]
    WHERE UserID = @UserID

--Insert Procedure
CREATE PROCEDURE [dbo].[PR_User_Insert]
@UserName VARCHAR(100),
@Email VARCHAR(100),
@Password VARCHAR(100),
@MobileNo VARCHAR(15),
@Address VARCHAR(100),
@IsActive BIT
AS
    INSERT INTO [User] (UserName, Email, [Password], MobileNo, Address, IsActive)
    VALUES (@UserName, @Email, @Password, @MobileNo, @Address, @IsActive)

--UpdateByPK Procedure
CREATE PROCEDURE [dbo].[PR_User_UpdateByPK]
@UserID INT,
@UserName VARCHAR(100),
@Email VARCHAR(100),
@Password VARCHAR(100),
@MobileNo VARCHAR(15),
@Address VARCHAR(100),
@IsActive BIT
AS
    UPDATE [User]
    SET UserName = @UserName,
        Email = @Email,
        [Password] = @Password,
        MobileNo = @MobileNo,
        [Address] = @Address,
        IsActive = @IsActive
    WHERE UserID = @UserID

--DeleteByPK Procedure
CREATE PROCEDURE [dbo].[PR_User_DeleteByPK]
@UserID INT
AS
    DELETE FROM [User]
    WHERE UserID = @UserID

--3. Order Stored Procedures

--SelectAll Procedure
CREATE PROCEDURE [dbo].[PR_Order_SelectAll]
AS
    SELECT 
        O.OrderID, 
        O.OrderDate, 
        C.CustomerName, 
        O.PaymentMode, 
        O.TotalAmount, 
        O.ShippingAddress, 
        U.UserName
    FROM [Order] O
    JOIN Customer C ON O.CustomerID = C.CustomerID
    JOIN [User] U ON O.UserID = U.UserID
    ORDER BY O.OrderDate

--SelectByPK Procedure
CREATE PROCEDURE [dbo].[PR_Order_SelectByPK]
@OrderID INT
AS
    SELECT 
        O.OrderID, 
        O.OrderDate, 
        O.CustomerID, 
        O.PaymentMode, 
        O.TotalAmount, 
        O.ShippingAddress, 
        O.UserID
    FROM [Order] O
    WHERE O.OrderID = @OrderID

--Insert Procedure
CREATE PROCEDURE [dbo].[PR_Order_Insert]
@OrderDate DATETIME,
@CustomerID INT,
@PaymentMode VARCHAR(100),
@TotalAmount DECIMAL(10,2),
@ShippingAddress VARCHAR(100),
@UserID INT
AS
    INSERT INTO [Order] (OrderDate, CustomerID, PaymentMode, TotalAmount, ShippingAddress, UserID)
    VALUES (@OrderDate, @CustomerID, @PaymentMode, @TotalAmount, @ShippingAddress, @UserID)

--UpdateByPK Procedure
CREATE PROCEDURE [dbo].[PR_Order_UpdateByPK]
@OrderID INT,
@OrderDate DATETIME,
@CustomerID INT,
@PaymentMode VARCHAR(100),
@TotalAmount DECIMAL(10,2),
@ShippingAddress VARCHAR(100),
@UserID INT
AS
    UPDATE [Order]
    SET OrderDate = @OrderDate,
        CustomerID = @CustomerID,
        PaymentMode = @PaymentMode,
        TotalAmount = @TotalAmount,
        ShippingAddress = @ShippingAddress,
        UserID = @UserID
    WHERE OrderID = @OrderID

--DeleteByPK Procedure
CREATE PROCEDURE [dbo].[PR_Order_DeleteByPK]
@OrderID INT
AS
    DELETE FROM [Order]
    WHERE OrderID = @OrderID

--4. OrderDetail Stored Procedures

--SelectAll Procedure
CREATE PROCEDURE [dbo].[PR_OrderDetail_SelectAll]
AS
    SELECT 
        OD.OrderDetailID, 
        OD.OrderID, 
        P.ProductName, 
        OD.Quantity, 
        OD.Amount, 
        OD.TotalAmount, 
        U.UserName
    FROM OrderDetail OD
    JOIN Product P ON OD.ProductID = P.ProductID
    JOIN [Order] O ON OD.OrderID = O.OrderID
    JOIN [User] U ON OD.UserID = U.UserID
    ORDER BY OD.OrderID

--SelectByPK Procedure
CREATE PROCEDURE [dbo].[PR_OrderDetail_SelectByPK]
@OrderDetailID INT
AS
    SELECT 
        OD.OrderDetailID, 
        OD.OrderID, 
        OD.ProductID, 
        OD.Quantity, 
        OD.Amount, 
        OD.TotalAmount, 
        OD.UserID
    FROM OrderDetail OD
    WHERE OD.OrderDetailID = @OrderDetailID

--Insert Procedure
CREATE PROCEDURE [dbo].[PR_OrderDetail_Insert]
@OrderID INT,
@ProductID INT,
@Quantity INT,
@Amount DECIMAL(10,2),
@TotalAmount DECIMAL(10,2),
@UserID INT
AS
    INSERT INTO OrderDetail (OrderID, ProductID, Quantity, Amount, TotalAmount, UserID)
    VALUES (@OrderID, @ProductID, @Quantity, @Amount, @TotalAmount, @UserID)

--UpdateByPK Procedure
CREATE PROCEDURE [dbo].[PR_OrderDetail_UpdateByPK]
@OrderDetailID INT,
@OrderID INT,
@ProductID INT,
@Quantity INT,
@Amount DECIMAL(10,2),
@TotalAmount DECIMAL(10,2),
@UserID INT
AS
    UPDATE OrderDetail
    SET OrderID = @OrderID,
        ProductID = @ProductID,
        Quantity = @Quantity,
        Amount = @Amount,
        TotalAmount = @TotalAmount,
        UserID = @UserID
    WHERE OrderDetailID = @OrderDetailID

--DeleteByPK Procedure
CREATE PROCEDURE [dbo].[PR_OrderDetail_DeleteByPK]
@OrderDetailID INT
AS
    DELETE FROM OrderDetail
    WHERE OrderDetailID = @OrderDetailID

--5. Bills Stored Procedures

--SelectAll Procedure
CREATE PROCEDURE [dbo].[PR_Bills_SelectAll]
AS
    SELECT 
        B.BillID, 
        B.BillNumber, 
        B.BillDate, 
        B.OrderID, 
        B.TotalAmount, 
        B.Discount, 
        B.NetAmount, 
        U.UserName
    FROM Bills B
    JOIN [Order] O ON B.OrderID = O.OrderID
    JOIN [User] U ON B.UserID = U.UserID
    ORDER BY B.BillDate

--SelectByPK Procedure
CREATE PROCEDURE [dbo].[PR_Bills_SelectByPK]
@BillID INT
AS
    SELECT 
        B.BillID, 
        B.BillNumber, 
        B.BillDate, 
        B.OrderID, 
        B.TotalAmount, 
        B.Discount, 
        B.NetAmount, 
        B.UserID
    FROM Bills B
    WHERE B.BillID = @BillID

--Insert Procedure
CREATE PROCEDURE [dbo].[PR_Bills_Insert]
@BillNumber VARCHAR(100),
@BillDate DATETIME,
@OrderID INT,
@TotalAmount DECIMAL(10,2),
@Discount DECIMAL(10,2),
@NetAmount DECIMAL(10,2),
@UserID INT
AS
    INSERT INTO Bills (BillNumber, BillDate, OrderID, TotalAmount, Discount, NetAmount, UserID)
    VALUES (@BillNumber, @BillDate, @OrderID, @TotalAmount, @Discount, @NetAmount, @UserID)

--UpdateByPK Procedure
CREATE PROCEDURE [dbo].[PR_Bills_UpdateByPK]
@BillID INT,
@BillNumber VARCHAR(100),
@BillDate DATETIME,
@OrderID INT,
@TotalAmount DECIMAL(10,2),
@Discount DECIMAL(10,2),
@NetAmount DECIMAL(10,2),
@UserID INT
AS
    UPDATE Bills
    SET BillNumber = @BillNumber,
        BillDate = @BillDate,
        OrderID = @OrderID,
        TotalAmount = @TotalAmount,
        Discount = @Discount,
        NetAmount = @NetAmount,
        UserID = @UserID
    WHERE BillID = @BillID

--DeleteByPK Procedure
CREATE PROCEDURE [dbo].[PR_Bills_DeleteByPK]
@BillID INT
AS
    DELETE FROM Bills
    WHERE BillID = @BillID

--6. Customer Stored Procedures

--SelectAll Procedure
CREATE PROCEDURE [dbo].[PR_Customer_SelectAll]
AS
    SELECT 
        C.CustomerID, 
        C.CustomerName, 
        C.HomeAddress, 
        C.Email, 
        C.MobileNo, 
        C.GST_NO, 
        C.CityName, 
        C.PinCode, 
        C.NetAmount, 
        U.UserName
    FROM Customer C
    JOIN [User] U ON C.UserID = U.UserID
    ORDER BY C.CustomerName

--SelectByPK Procedure
CREATE PROCEDURE [dbo].[PR_Customer_SelectByPK]
@CustomerID INT
AS
    SELECT 
        C.CustomerID, 
        C.CustomerName, 
        C.HomeAddress, 
        C.Email, 
        C.MobileNo, 
        C.GST_NO, 
        C.CityName, 
        C.PinCode, 
        C.NetAmount, 
        C.UserID
    FROM Customer C
    WHERE C.CustomerID = @CustomerID

--Insert Procedure
CREATE PROCEDURE [dbo].[PR_Customer_Insert]
@CustomerName VARCHAR(100),
@HomeAddress VARCHAR(100),
@Email VARCHAR(100),
@MobileNo VARCHAR(15),
@GST_NO VARCHAR(15),
@CityName VARCHAR(100),
@PinCode VARCHAR(15),
@NetAmount DECIMAL(10,2),
@UserID INT
AS
    INSERT INTO Customer (CustomerName, HomeAddress, Email, MobileNo, GST_NO, CityName, PinCode, NetAmount, UserID)
    VALUES (@CustomerName, @HomeAddress, @Email, @MobileNo, @GST_NO, @CityName, @PinCode, @NetAmount, @UserID)

--UpdateByPK Procedure
CREATE PROCEDURE [dbo].[PR_Customer_UpdateByPK]
@CustomerID INT,
@CustomerName VARCHAR(100),
@HomeAddress VARCHAR(100),
@Email VARCHAR(100),
@MobileNo VARCHAR(15),
@GST_NO VARCHAR(15),
@CityName VARCHAR(100),
@PinCode VARCHAR(15),
@NetAmount DECIMAL(10,2),
@UserID INT
AS
    UPDATE Customer
    SET CustomerName = @CustomerName,
        HomeAddress = @HomeAddress,
        Email = @Email,
        MobileNo = @MobileNo,
        GST_NO = @GST_NO,
        CityName = @CityName,
        PinCode = @PinCode,
        NetAmount = @NetAmount,
        UserID = @UserID
    WHERE CustomerID = @CustomerID

--DeleteByPK Procedure
CREATE PROCEDURE [dbo].[PR_Customer_DeleteByPK]
@CustomerID INT
AS
    DELETE FROM Customer
    WHERE CustomerID = @CustomerID
