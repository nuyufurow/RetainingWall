function [ Ass ] =zhengjinshuchu150( Asmax )
%本子函数输出正筋
%   此处显示详细说明
AA00=[12 150 754;
      14 150 1026;
      16 150 1340;
      18 150 1696;
      20 150 2094;
      22 150 2534;
      25 150 3272];

  if Asmax<=AA00(1,3)
      Ass=[AA00(1,1) AA00(1,2) AA00(1,3)];
  elseif Asmax>AA00(1,3) && Asmax<=AA00(2,3)
      Ass=[AA00(2,1) AA00(2,2) AA00(2,3)];
  elseif Asmax>AA00(2,3) && Asmax<=AA00(3,3)
      Ass=[AA00(3,1) AA00(3,2) AA00(3,3)];
  elseif Asmax>AA00(3,3) && Asmax<=AA00(4,3)
      Ass=[AA00(4,1) AA00(4,2) AA00(4,3)];  
  elseif Asmax>AA00(4,3) && Asmax<=AA00(5,3)
      Ass=[AA00(5,1) AA00(5,2) AA00(5,3)];  
  elseif Asmax>AA00(5,3) && Asmax<=AA00(6,3)
      Ass=[AA00(6,1) AA00(6,2) AA00(6,3)];       
   elseif Asmax>AA00(6,3) && Asmax<=AA00(7,3)
      Ass=[AA00(7,1) AA00(7,2) AA00(7,3)];     
  end     
end

