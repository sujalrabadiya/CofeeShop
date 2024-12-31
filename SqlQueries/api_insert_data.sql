INSERT INTO Country (CountryName, CountryCode, CreatedDate) VALUES
('United States', 'US', GETDATE()),
('India', 'IN', GETDATE()),
('Australia', 'AU', GETDATE()),
('Canada', 'CA', GETDATE()),
('United Kingdom', 'UK', GETDATE()),
('Germany', 'DE', GETDATE()),
('France', 'FR', GETDATE()),
('Japan', 'JP', GETDATE()),
('China', 'CN', GETDATE()),
('Brazil', 'BR', GETDATE());

INSERT INTO State (StateName, StateCode, CountryID, CreatedDate) VALUES
('California', 'CA', 1, GETDATE()),
('Texas', 'TX', 1, GETDATE()),
('Gujarat', 'GJ', 2, GETDATE()),
('Maharashtra', 'MH', 2, GETDATE()),
('New South Wales', 'NSW', 3, GETDATE()),
('Victoria', 'VIC', 3, GETDATE()),
('Ontario', 'ON', 4, GETDATE()),
('Quebec', 'QC', 4, GETDATE()),
('England', 'ENG', 5, GETDATE()),
('Scotland', 'SCT', 5, GETDATE());

INSERT INTO City (CityName, CityCode, StateID, CountryID, CreatedDate) VALUES
('Los Angeles', 'LA', 1, 1, GETDATE()),
('Houston', 'HOU', 2, 1, GETDATE()),
('Ahmedabad', 'AMD', 3, 2, GETDATE()),
('Mumbai', 'MUM', 4, 2, GETDATE()),
('Sydney', 'SYD', 5, 3, GETDATE()),
('Melbourne', 'MEL', 6, 3, GETDATE()),
('Toronto', 'TOR', 7, 4, GETDATE()),
('Montreal', 'MTL', 8, 4, GETDATE()),
('London', 'LDN', 9, 5, GETDATE()),
('Edinburgh', 'EDI', 10, 5, GETDATE());
