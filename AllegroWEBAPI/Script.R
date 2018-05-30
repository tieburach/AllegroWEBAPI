source("eksplo.R")
library(RODBC)

  myconn <- odbcConnect("allegro", uid = "Tieburach", pwd = "")
  pundat <- sqlQuery(myconn, "select * from AllegroDatabase.dbo.ProductsPrices")
  
  prices <- pundat[,c("price")]
  parameters <- pundat[,c("parametervalues")]
  prices <- sort(prices)
  y <- dnorm(prices, mean=mean(prices), sd = sd(prices))
  cenamin <- mean(prices) - sd(prices)
  cenaavg <- mean(prices)
  cenamax <- mean(prices) + sd(prices)
  cenaminwykres <- prices[which(abs(prices-cenamin)==min(abs(prices-cenamin)))]
  cenaavgwykres <- prices[which(abs(prices-cenaavg)==min(abs(prices-cenaavg)))]
  cenamaxwykres <- prices[which(abs(prices-cenamax)==min(abs(prices-cenamax)))]
 
  #rysowanie rozkladu normalnego z zaznaczonymi programi
  X11()
  plot(prices, y, col=ifelse(prices==cenaminwykres | prices==cenaavgwykres | prices==cenamaxwykres, "red", "white"))
  lines(prices, y)
  dev.copy(jpeg, filename="rozkladnormalny.jpg")
  dev.off()
  
  #rysowanie histogramu dla ceny
  X11()
  hist(prices)
  dev.copy(jpeg, filename="histogramceny.jpg")
  dev.off()
  
  #rysowanie histogramu dla wybranego parametru
  X11()
  hist(parameters)
  dev.copy(jpeg, filename="histogramparametrow.jpg")
  dev.off()
  
  
  