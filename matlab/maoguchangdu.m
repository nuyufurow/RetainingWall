function [ la ] =maoguchangdu( C,F,d )
%此子函数用于储存不同钢筋等级、混凝土等级的la
% C——为混凝土标号
% F——为钢筋等级
% d——为钢筋直径
   if C==20 && F==300
       la=39*d;
   elseif C==20 && F==335
       la=38*d;
       
   elseif C==25 && F==300
       la=34*d;
   elseif C==25 && F==335
       la=33*d; 
   elseif C==25 && F==400
       la=40*d; 
   elseif C==25 && F==500
       la=48*d;
       
   elseif C==30 && F==300
       la=30*d;   
   elseif C==30 && F==335
       la=29*d;
   elseif C==30 && F==400
       la=35*d;       
   elseif C==30 && F==500
       la=43*d; 
       
   elseif C==35 && F==300
       la=28*d;       
   elseif C==35 && F==335
       la=27*d;       
   elseif C==35 && F==400
       la=32*d;        
   elseif C==35 && F==500
       la=39*d;
       
   elseif C==40 && F==300
       la=25*d;       
   elseif C==40 && F==335
       la=25*d;        
   elseif C==40 && F==400
       la=29*d;       
   elseif C==40 && F==500
       la=36*d; 
       
   elseif C==45 && F==300
       la=24*d; 
   elseif C==45 && F==335
       la=23*d;        
   elseif C==45 && F==400
       la=28*d;       
   elseif C==45 && F==500
       la=34*d;
       
   elseif C==50 && F==300
       la=23*d; 
   elseif C==50 && F==335
       la=22*d;        
   elseif C==50 && F==400
       la=27*d;       
   elseif C==50 && F==500
       la=32*d;    
   end
   
end
