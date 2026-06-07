Create   Procedure sp_DELETEPRODUCT  
(  
@PRODUCTID int,  
@OUTPUTMESSAGE nvarchar(50) OUTPUT  
  
)  
AS  
BEGIN  
  
 DECLARE @RowCount int = 0  
  
 BEGIN TRY  
  
 SET @RowCount = (select count(1) from dbo.tbl_ProductMaster where PRODUCTID = @PRODUCTID )  
  IF(@RowCount > 0)  
   BEGIN  
  
    BEGIN TRAN  
  
     DELETE FROM dbo.tbl_ProductMaster  
     WHERE PRODUCTID = @PRODUCTID  
  
     SET @OUTPUTMESSAGE = 'Product deleted Successfully...! '  
  
    COMMIT TRAN  
   END   
  ELSE  
   BEGIN  
    SET @OUTPUTMESSAGE = 'Product not available with id' + Convert(varchar, @PRODUCTID)  
   END  
  END TRY  
  BEGIN CATCH  
  ROLLBACK TRAN  
   SET @OUTPUTMESSAGE = ERROR_MESSAGE()  
  
  END CATCH  
END