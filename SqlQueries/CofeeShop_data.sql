-- User

INSERT INTO dbo.[User] (UserName, Email, Password, MobileNo, Address, IsActive)
VALUES 
('John Doe', 'john.doe@example.com', 'password123', '1234567890', '123 Main St', 1),
('Jane Smith', 'jane.smith@example.com', 'password456', '0987654321', '456 Elm St', 1),
('Alice Johnson', 'alice.johnson@example.com', 'password789', '1122334455', '789 Pine St', 0),
('Bob Brown', 'bob.brown@example.com', 'password321', '2233445566', '321 Oak St', 1),
('Charlie Davis', 'charlie.davis@example.com', 'password654', '3344556677', '654 Cedar St', 0),
('David Evans', 'david.evans@example.com', 'password111', '4455667788', '111 Maple St', 1),
('Eva Green', 'eva.green@example.com', 'password222', '5566778899', '222 Birch St', 0),
('Frank Harris', 'frank.harris@example.com', 'password333', '6677889900', '333 Willow St', 1),
('Grace Lee', 'grace.lee@example.com', 'password444', '7788990011', '444 Spruce St', 1),
('Henry King', 'henry.king@example.com', 'password555', '8899001122', '555 Cherry St', 0);


-- Order Table

  INSERT INTO dbo.[Order] (OrderDate, CustomerName, PaymentMode, TotalAmount, ShippingAddress, UserID)
  VALUES 
  ('2023-07-01 10:30:00', 1, 'Credit Card', 150.75, '123 Main St', 1),
('2023-07-02 14:00:00', 2, 'PayPal', 200.00, '456 Elm St', 2),
('2023-07-03 09:15:00', 3, 'Credit Card', 120.00, '789 Pine St', 3),
('2023-07-04 11:45:00', 4, 'Cash', 99.99, '321 Oak St', 4),
('2023-07-05 16:20:00', 5, 'Debit Card', 175.50, '654 Cedar St', 5),
('2023-07-06 12:00:00', 1, 'Credit Card', 220.75, '123 Main St', 1),
('2023-07-07 08:45:00', 2, 'PayPal', 300.00, '456 Elm St', 2),
('2023-07-08 17:30:00', 3, 'Cash', 180.25, '789 Pine St', 3),
('2023-07-09 13:10:00', 4, 'Credit Card', 210.00, '321 Oak St', 4),
('2023-07-10 10:50:00', 5, 'Debit Card', 250.00, '654 Cedar St', 5);


-- Product

INSERT INTO dbo.Product (ProductName, ProductPrice, ProductCode, Description, UserID)
VALUES 
('Product A', 10.00, 'PRA100', 'Description of Product A', 1),
('Product B', 20.00, 'PRB200', 'Description of Product B', 2),
('Product C', 30.00, 'PRC300', 'Description of Product C', 1),
('Product D', 40.00, 'PRD400', 'Description of Product D', 3),
('Product E', 50.00, 'PRE500', 'Description of Product E', 2),
('Product F', 60.00, 'PRF600', 'Description of Product F', 3),
('Product G', 70.00, 'PRG700', 'Description of Product G', 1),
('Product H', 80.00, 'PRH800', 'Description of Product H', 2),
('Product I', 90.00, 'PRI900', 'Description of Product I', 3),
('Product J', 100.00, 'PRJ1000', 'Description of Product J', 1);


-- Order Detail

INSERT INTO dbo.OrderDetail (OrderID, ProductID, Quantity, Amount, TotalAmount, UserID)
VALUES 
(1, 1, 1, 10.00, 10.00, 1),
(1, 2, 2, 20.00, 40.00, 1),
(2, 3, 1, 30.00, 30.00, 2),
(2, 4, 2, 40.00, 80.00, 2),
(3, 5, 1, 50.00, 50.00, 3),
(3, 1, 3, 10.00, 30.00, 3),
(4, 2, 2, 20.00, 40.00, 1),
(4, 3, 1, 30.00, 30.00, 1),
(5, 4, 2, 40.00, 80.00, 2),
(5, 5, 1, 50.00, 50.00, 2);


-- Insert records into dbo.Bills table

INSERT INTO dbo.Bills (BillNumber, BillDate, OrderID, TotalAmount, Discount, NetAmount, UserID)
VALUES 
('BILL001', '2024-07-01', 1, 100.00, 5.00, 95.00, 1),
('BILL002', '2024-07-02', 2, 200.00, 10.00, 190.00, 2),
('BILL003', '2024-07-03', 3, 300.00, 15.00, 285.00, 3),
('BILL004', '2024-07-04', 4, 150.00, NULL, 150.00, 1),
('BILL005', '2024-07-05', 5, 250.00, 12.50, 237.50, 2),
('BILL006', '2024-07-06', 1, 120.00, 6.00, 114.00, 1),
('BILL007', '2024-07-07', 2, 220.00, 11.00, 209.00, 2),
('BILL008', '2024-07-08', 3, 320.00, 16.00, 304.00, 3),
('BILL009', '2024-07-09', 4, 180.00, 9.00, 171.00, 1),
('BILL010', '2024-07-10', 5, 270.00, 13.50, 256.50, 2);

-- Insert records into dbo.Customer table

INSERT INTO dbo.Customer (CustomerName, HomeAddress, Email, MobileNo, GST_NO, CityName, PinCode, NetAmount, UserID)
VALUES 
('Alice Green', '789 Pine St', 'alice.green@example.com', '1234567890', 'GST1234567890', 'Pine City', '123456', 1000.00, 1),
('Bob White', '321 Oak St', 'bob.white@example.com', '0987654321', 'GST0987654321', 'Oak Town', '654321', 2000.00, 2),
('Charlie Black', '456 Elm St', 'charlie.black@example.com', '1122334455', 'GST1122334455', 'Elm Village', '789012', 1500.00, 3),
('David Blue', '654 Cedar St', 'david.blue@example.com', '2233445566', 'GST2233445566', 'Cedar Grove', '345678', 2500.00, 4),
('Emma Yellow', '123 Main St', 'emma.yellow@example.com', '3344556677', 'GST3344556677', 'Main City', '567890', 3000.00, 5),
('Frank Orange', '789 Birch St', 'frank.orange@example.com', '4455667788', 'GST4455667788', 'Birch Town', '678901', 1750.00, 1),
('Grace Purple', '321 Willow St', 'grace.purple@example.com', '5566778899', 'GST5566778899', 'Willow Grove', '890123', 2250.00, 2),
('Henry Brown', '456 Maple St', 'henry.brown@example.com', '6677889900', 'GST6677889900', 'Maple Village', '901234', 2750.00, 3),
('Isabel Silver', '654 Spruce St', 'isabel.silver@example.com', '7788990011', 'GST7788990011', 'Spruce Town', '123789', 3250.00, 4),
('Jack Gold', '123 Cedar St', 'jack.gold@example.com', '8899001122', 'GST8899001122', 'Cedar City', '345012', 3500.00, 5);