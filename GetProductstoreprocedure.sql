CREATE OR ALTER PROCEDURE sp_GetProducts
AS
BEGIN
    SELECT ProductID,
           ProductName,
           Price,
           Qty,
           Remarks
    FROM dbo.tbl_ProductMaster
END