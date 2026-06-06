create or alter procedure sp_InsertProducts
(
@ProductName nvarchar(50),
@price decimal(8,2),
@Qty int,
@Remarks nvarchar(50)

)
as
begin
declare @RowCount int = 0

set @RowCount = (select count(1) from dbo.tbl_ProductMaster where ProductName = @ProductName)

	begin try
		begin tran

		if(@RowCount = 0)
			begin
				insert into dbo.tbl_ProductMaster(ProductName,Price,Qty,Remarks)
				values(@ProductName,@price,@Qty,@Remarks)
			end
		Commit tran	
	end try 

	begin catch
	rollback tran
	select ERROR_MESSAGE()

	end catch
end