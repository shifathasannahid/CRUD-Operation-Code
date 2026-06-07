Create   procedure [dbo].[sp_UpdateProducts]
(
@ProductID int,
@ProductName nvarchar(50),
@price decimal(8,2),
@Qty int,
@Remarks nvarchar(50) 

)
as
begin
declare @RowCount int = 0

set @RowCount = (select count(1) from dbo.tbl_ProductMaster where ProductName = @ProductName and ProductID <> @ProductID)

	begin try
		begin tran

		if(@RowCount = 0)
			begin
			update dbo.tbl_ProductMaster
				set ProductName =  @ProductName,
					Price       =  @price,
					Qty			=  @Qty,
					Remarks		=  @Remarks
				where PRODUCTID = @ProductID

				
			end
		Commit tran	
	end try 

	begin catch
	rollback tran
	select ERROR_MESSAGE()

	end catch
end