create or alter procedure sp_GetProductByID
(
@ProductID int
)
as
Begin 
	Select	 PRODUCTID
		    ,[ProductName]
			,[Price]
			,[Qty]
			,[Remarks]
  FROM [ADO_EXAMPLE].[dbo].[tbl_ProductMaster]
  where PRODUCTID =@ProductID

End