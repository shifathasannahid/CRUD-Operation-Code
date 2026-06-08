Create or Alter Procedure sp_ProductCRUD
(
	@Action Varchar(10),
	@ProductID int = Null,
	@ProductName nvarchar(50) = Null,
	@Price decimal(8,2) = Null,
	@Qty int = Null,
	@Remarks nvarchar(50) = Null,
	@OUTPUTMESSAGE nvarchar(100) = null OUTPUT
)

as
begin
	
	if @Action = 'INSERT'
	begin
		insert into tbl_ProductMaster(ProductName, Price, Qty, Remarks)
		values (@ProductName, @Price, @Qty, @Remarks)
	end

	else if @Action = 'UPDATE'
	begin
		update tbl_ProductMaster
		set ProductName = @ProductName,
			Price   = @Price,
			Qty     = @Qty,
			Remarks = @Remarks
		where ProductID = @ProductID
	end

	else if @Action = 'DELETE'
	begin
		delete from tbl_ProductMaster
		where ProductID = @ProductID

		SET @OUTPUTMESSAGE = 'Product Deleted Successfully'
	end

end