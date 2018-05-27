library(RODBC)
myconn <- odbcConnect("allegro", uid = "Tieburach", pwd = "")
pundat <- sqlQuery(myconn, "select * from AllegroDatabase.dbo.ProductsPrices")
close(myconn)
