function [ Ass ] = zuhezhengfujin( n,As,ft,fy,h,M,cs,C,rg )
%此子函数用于将正筋选筋和负筋选筋结果组合成一个矩阵，方便后续调用
%Asz——为正筋选筋矩阵
%Asf150——为负筋选筋矩阵间距150
%Asf200——为负筋选筋矩阵间距200
%Ass——为最后输出的配筋
%As——为为peijinjisuan( M,cs,n,fy,fc,ft)中计算得到各点的计算配筋
if n==1
        Asz150=zhengjinxuanjin150(n,As,h,M,cs,C,rg); 
        Asz200=zhengjinxuanjin200(n,As,h,M,cs,C,rg);
        Asf150=fujinxuanjin150(n,As,ft,fy,h,M,cs,C,rg);    
        Asf200=fujinxuanjin200(n,As,ft,fy,h,M,cs,C,rg); 
        Ass=[Asf150;
             Asf200;
             Asz150;
             Asz200];
    
elseif n==2
        Asz150=zhengjinxuanjin150(n,As,h,M,cs,C,rg);
        Asz200=zhengjinxuanjin200(n,As,h,M,cs,C,rg);
        Asf150=fujinxuanjin150(n,As,ft,fy,h,M,cs,C,rg);    
        Asf200=fujinxuanjin200(n,As,ft,fy,h,M,cs,C,rg); 
        Ass=[Asf150;
             Asf200;
             Asz150;
             Asz200];   
   
elseif n==3
        Asz150=zhengjinxuanjin150(n,As,h,M,cs,C,rg); 
        Asz200=zhengjinxuanjin200(n,As,h,M,cs,C,rg);
        Asf150=fujinxuanjin150(n,As,ft,fy,h,M,cs,C,rg);    
        Asf200=fujinxuanjin200(n,As,ft,fy,h,M,cs,C,rg); 
        Ass=[Asf150;
             Asf200;
             Asz150;
             Asz200];   
   
elseif n==4
        Asz150=zhengjinxuanjin150(n,As,h,M,cs,C,rg); 
        Asz200=zhengjinxuanjin200(n,As,h,M,cs,C,rg);
        Asf150=fujinxuanjin150(n,As,ft,fy,h,M,cs,C,rg);    
        Asf200=fujinxuanjin200(n,As,ft,fy,h,M,cs,C,rg); 
        Ass=[Asf150;
             Asf200;
             Asz150;
             Asz200];
end
end