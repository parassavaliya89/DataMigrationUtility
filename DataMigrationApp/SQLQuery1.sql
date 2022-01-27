Declare @Id int
Set @Id = 1

While(@Id <= 1000000)
Begin
Insert into Source values(RAND()*(1000000 - 1 + 1)+1,RAND()*(1000000-1+1)+1)
print @Id
Set @Id = @Id + 1
End

Select COUNT(*) from Source