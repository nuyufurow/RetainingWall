function [ laE ] =maoguchangduE( Z,C,F,d )
%���Ӻ������ڴ��治ͬ�ֽ�ȼ����������ȼ��Ϳ���ȼ���laE
% Z����Ϊ����ȼ�
% C����Ϊ���������
% F����Ϊ�ֽ�ȼ�
% d����Ϊ�ֽ�ֱ��
if Z==4   %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%����ȼ�Ϊ4��
   if C==20 && F==300
       laE=39*d;
   elseif C==20 && F==335
       laE=38*d;
       
   elseif C==25 && F==300
       laE=34*d;
   elseif C==25 && F==335
       laE=33*d; 
   elseif C==25 && F==400
       laE=40*d; 
   elseif C==25 && F==500
       laE=48*d;
       
   elseif C==30 && F==300
       laE=30*d;   
   elseif C==30 && F==335
       laE=29*d;
   elseif C==30 && F==400
       laE=35*d;       
   elseif C==30 && F==500
       laE=43*d; 
       
   elseif C==35 && F==300
       laE=28*d;       
   elseif C==35 && F==335
       laE=27*d;       
   elseif C==35 && F==400
       laE=32*d;        
   elseif C==35 && F==500
       laE=39*d;
       
   elseif C==40 && F==300
       laE=25*d;       
   elseif C==40 && F==335
       laE=25*d;        
   elseif C==40 && F==400
       laE=29*d;       
   elseif C==40 && F==500
       laE=36*d; 
       
   elseif C==45 && F==300
       laE=24*d; 
   elseif C==45 && F==335
       laE=23*d;        
   elseif C==45 && F==400
       laE=28*d;       
   elseif C==45 && F==500
       laE=34*d;
       
   elseif C==50 && F==300
       laE=23*d; 
   elseif C==50 && F==335
       laE=22*d;        
   elseif C==50 && F==400
       laE=27*d;       
   elseif C==50 && F==500
       laE=32*d;    
   end
   
elseif Z==3              %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%����ȼ�Ϊ3��
   if C==20 && F==300
       laE=41*d;
   elseif C==20 && F==335
       laE=40*d;
       
   elseif C==25 && F==300
       laE=36*d;
   elseif C==25 && F==335
       laE=35*d; 
   elseif C==25 && F==400
       laE=42*d; 
   elseif C==25 && F==500
       laE=50*d;
       
   elseif C==30 && F==300
       laE=32*d;   
   elseif C==30 && F==335
       laE=30*d;
   elseif C==30 && F==400
       laE=37*d;       
   elseif C==30 && F==500
       laE=45*d; 
       
   elseif C==35 && F==300
       laE=29*d;       
   elseif C==35 && F==335
       laE=28*d;       
   elseif C==35 && F==400
       laE=34*d;        
   elseif C==35 && F==500
       laE=41*d;
       
   elseif C==40 && F==300
       laE=26*d;       
   elseif C==40 && F==335
       laE=26*d;        
   elseif C==40 && F==400
       laE=30*d;       
   elseif C==40 && F==500
       laE=38*d; 
       
   elseif C==45 && F==300
       laE=25*d; 
   elseif C==45 && F==335
       laE=24*d;        
   elseif C==45 && F==400
       laE=29*d;       
   elseif C==45 && F==500
       laE=36*d;
       
   elseif C==50 && F==300
       laE=24*d; 
   elseif C==50 && F==335
       laE=23*d;        
   elseif C==50 && F==400
       laE=28*d;       
   elseif C==50 && F==500
       laE=34*d;    
   end    

   elseif Z==2           %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%����ȼ�Ϊ2��
   if C==20 && F==300
       laE=45*d;
   elseif C==20 && F==335
       laE=44*d;
       
   elseif C==25 && F==300
       laE=39*d;
   elseif C==25 && F==335
       laE=38*d; 
   elseif C==25 && F==400
       laE=46*d; 
   elseif C==25 && F==500
       laE=55*d;
       
   elseif C==30 && F==300
       laE=35*d;   
   elseif C==30 && F==335
       laE=33*d;
   elseif C==30 && F==400
       laE=40*d;       
   elseif C==30 && F==500
       laE=49*d; 
       
   elseif C==35 && F==300
       laE=32*d;       
   elseif C==35 && F==335
       laE=31*d;       
   elseif C==35 && F==400
       laE=37*d;        
   elseif C==35 && F==500
       laE=45*d;
       
   elseif C==40 && F==300
       laE=29*d;       
   elseif C==40 && F==335
       laE=29*d;        
   elseif C==40 && F==400
       laE=33*d;       
   elseif C==40 && F==500
       laE=41*d; 
       
   elseif C==45 && F==300
       laE=28*d; 
   elseif C==45 && F==335
       laE=26*d;        
   elseif C==45 && F==400
       laE=32*d;       
   elseif C==45 && F==500
       laE=39*d;
       
   elseif C==50 && F==300
       laE=26*d; 
   elseif C==50 && F==335
       laE=25*d;        
   elseif C==50 && F==400
       laE=31*d;       
   elseif C==50 && F==500
       laE=37*d;    
   end    

   elseif Z==1           %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%����ȼ�Ϊ1��
   if C==20 && F==300
       laE=45*d;
   elseif C==20 && F==335
       laE=44*d;
       
   elseif C==25 && F==300
       laE=39*d;
   elseif C==25 && F==335
       laE=38*d; 
   elseif C==25 && F==400
       laE=46*d; 
   elseif C==25 && F==500
       laE=55*d;
       
   elseif C==30 && F==300
       laE=35*d;   
   elseif C==30 && F==335
       laE=33*d;
   elseif C==30 && F==400
       laE=40*d;       
   elseif C==30 && F==500
       laE=49*d; 
       
   elseif C==35 && F==300
       laE=32*d;       
   elseif C==35 && F==335
       laE=31*d;       
   elseif C==35 && F==400
       laE=37*d;        
   elseif C==35 && F==500
       laE=45*d;
       
   elseif C==40 && F==300
       laE=29*d;       
   elseif C==40 && F==335
       laE=29*d;        
   elseif C==40 && F==400
       laE=33*d;       
   elseif C==40 && F==500
       laE=41*d; 
       
   elseif C==45 && F==300
       laE=28*d; 
   elseif C==45 && F==335
       laE=26*d;        
   elseif C==45 && F==400
       laE=32*d;       
   elseif C==45 && F==500
       laE=39*d;
       
   elseif C==50 && F==300
       laE=26*d; 
   elseif C==50 && F==335
       laE=25*d;        
   elseif C==50 && F==400
       laE=31*d;       
   elseif C==50 && F==500
       laE=37*d;    
   end       
   
end

end

