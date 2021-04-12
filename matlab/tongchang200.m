function [ At ] = tongchang200( ft,fy,h )
%此子函数用于给顶点选通长筋
%h为指定某一层的截面厚度
roumin=max(0.002,0.45*ft/(100*fy));        %最小配筋率
AAA=[12  200   0  0  565   0;               %第6列全部为0是裂缝宽度为0，为了和选筋子程序里的格式统一
     14  200   0  0  770   0;
     16  200   0  0  1005  0;
     18  200   0  0  1272  0;
     20  200   0  0  1571  0;
     22  200   0  0  1901  0;
     25  200   0  0  2454  0];
As=1000*h*roumin;

if As<=AAA(1,5)
    At=[AAA(1,1) AAA(1,2) AAA(1,3) AAA(1,4) AAA(1,5) AAA(1,6)];
elseif As>AAA(1,5) && As<=AAA(2,5)
    At=[AAA(2,1) AAA(2,2) AAA(2,3) AAA(2,4) AAA(2,5) AAA(2,6)];
elseif As>AAA(2,5) && As<=AAA(3,5)
    At=[AAA(3,1) AAA(3,2) AAA(3,3) AAA(3,4) AAA(3,5) AAA(3,6)];    
elseif As>AAA(3,5) && As<=AAA(4,5)
    At=[AAA(4,1) AAA(4,2) AAA(4,3) AAA(4,4) AAA(4,5) AAA(4,6)];     
elseif As>AAA(4,5) && As<=AAA(5,5)
    At=[AAA(5,1) AAA(5,2) AAA(5,3) AAA(5,4) AAA(5,5) AAA(5,6)];
elseif As>AAA(5,5) && As<=AAA(6,5)
    At=[AAA(6,1) AAA(6,2) AAA(6,3) AAA(6,4) AAA(6,5) AAA(6,6)];    
elseif As>AAA(6,5) && As<=AAA(7,5)
    At=[AAA(7,1) AAA(7,2) AAA(7,3) AAA(7,4) AAA(7,5) AAA(7,6)];   
end
   
end



