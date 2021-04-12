function [ As ] = shuipingjin( n,h )
%此子函数用于给水平筋选筋
%h为指定某一层的截面厚度
roumin=0.15/100;            %水平分布筋单侧最小配筋率0.15%
AAA=[12  150   0  0  754  0;               %第6列全部为0是裂缝宽度为0，为了和选筋子程序里的格式统一
     14  150   0  0  1026 0;
     16  150   0  0  1340 0;
     18  150   0  0  1696 0;
     20  150   0  0  2094 0;
     22  150   0  0  2534 0;
     25  150   0  0  3272 0];
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%1层挡墙
if n==1
    Ass(1)=1000*h(1)*roumin;
if Ass(1)<=AAA(1,5)
    As=[AAA(1,1) AAA(1,2) AAA(1,3) AAA(1,4) AAA(1,5) AAA(1,6)];
elseif Ass(1)>AAA(1,5) && Ass(1)<=AAA(2,5)
    As=[AAA(2,1) AAA(2,2) AAA(2,3) AAA(2,4) AAA(2,5) AAA(2,6)];
elseif Ass(1)>AAA(2,5) && Ass(1)<=AAA(3,5)
    As=[AAA(3,1) AAA(3,2) AAA(3,3) AAA(3,4) AAA(3,5) AAA(3,6)];    
elseif Ass(1)>AAA(3,5) && Ass(1)<=AAA(4,5)
    As=[AAA(4,1) AAA(4,2) AAA(4,3) AAA(4,4) AAA(4,5) AAA(4,6)];     
elseif Ass(1)>AAA(4,5) && Ass(1)<=AAA(5,5)
    As=[AAA(5,1) AAA(5,2) AAA(5,3) AAA(5,4) AAA(5,5) AAA(5,6)];
elseif Ass(1)>AAA(5,5) && Ass(1)<=AAA(6,5)
    As=[AAA(6,1) AAA(6,2) AAA(6,3) AAA(6,4) AAA(6,5) AAA(6,6)];    
elseif Ass(1)>AAA(6,5) && Ass(1)<=AAA(7,5)
    As=[AAA(7,1) AAA(7,2) AAA(7,3) AAA(7,4) AAA(7,5) AAA(7,6)]; 
end
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%2层挡墙
elseif n==2
       Ass(1)=1000*h(1)*roumin;
       Ass(2)=1000*h(2)*roumin;      
if Ass(1)<=AAA(1,5)
    As(1,:)=[AAA(1,1) AAA(1,2) AAA(1,3) AAA(1,4) AAA(1,5) AAA(1,6)];
elseif Ass(1)>AAA(1,5) && Ass(1)<=AAA(2,5)
    As(1,:)=[AAA(2,1) AAA(2,2) AAA(2,3) AAA(2,4) AAA(2,5) AAA(2,6)];
elseif Ass(1)>AAA(2,5) && Ass(1)<=AAA(3,5)
    As(1,:)=[AAA(3,1) AAA(3,2) AAA(3,3) AAA(3,4) AAA(3,5) AAA(3,6)];    
elseif Ass(1)>AAA(3,5) && Ass(1)<=AAA(4,5)
    As(1,:)=[AAA(4,1) AAA(4,2) AAA(4,3) AAA(4,4) AAA(4,5) AAA(4,6)];     
elseif Ass(1)>AAA(4,5) && Ass(1)<=AAA(5,5)
    As(1,:)=[AAA(5,1) AAA(5,2) AAA(5,3) AAA(5,4) AAA(5,5) AAA(5,6)];
elseif Ass(1)>AAA(5,5) && Ass(1)<=AAA(6,5)
    As(1,:)=[AAA(6,1) AAA(6,2) AAA(6,3) AAA(6,4) AAA(6,5) AAA(6,6)];    
elseif Ass(1)>AAA(6,5) && Ass(1)<=AAA(7,5)
    As(1,:)=[AAA(7,1) AAA(7,2) AAA(7,3) AAA(7,4) AAA(7,5) AAA(7,6)]; 
end
 
if Ass(2)<=AAA(1,5)
    As(2,:)=[AAA(1,1) AAA(1,2) AAA(1,3) AAA(1,4) AAA(1,5) AAA(1,6)];
elseif Ass(2)>AAA(1,5) && Ass(2)<=AAA(2,5)
    As(2,:)=[AAA(2,1) AAA(2,2) AAA(2,3) AAA(2,4) AAA(2,5) AAA(2,6)];
elseif Ass(2)>AAA(2,5) && Ass(2)<=AAA(3,5)
    As(2,:)=[AAA(3,1) AAA(3,2) AAA(3,3) AAA(3,4) AAA(3,5) AAA(3,6)];    
elseif Ass(2)>AAA(3,5) && Ass(2)<=AAA(4,5)
    As(2,:)=[AAA(4,1) AAA(4,2) AAA(4,3) AAA(4,4) AAA(4,5) AAA(4,6)];     
elseif Ass(2)>AAA(4,5) && Ass(2)<=AAA(5,5)
    As(2,:)=[AAA(5,1) AAA(5,2) AAA(5,3) AAA(5,4) AAA(5,5) AAA(5,6)];
elseif Ass(2)>AAA(5,5) && Ass(2)<=AAA(6,5)
    As(2,:)=[AAA(6,1) AAA(6,2) AAA(6,3) AAA(6,4) AAA(6,5) AAA(6,6)];    
elseif Ass(2)>AAA(6,5) && Ass(2)<=AAA(7,5)
    As(2,:)=[AAA(7,1) AAA(7,2) AAA(7,3) AAA(7,4) AAA(7,5) AAA(7,6)]; 
end    
 %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%3层挡墙       
elseif n==3    
       Ass(1)=1000*h(1)*roumin;
       Ass(2)=1000*h(2)*roumin; 
       Ass(3)=1000*h(3)*roumin;       
if Ass(1)<=AAA(1,5)
    As(1,:)=[AAA(1,1) AAA(1,2) AAA(1,3) AAA(1,4) AAA(1,5) AAA(1,6)];
elseif Ass(1)>AAA(1,5) && Ass(1)<=AAA(2,5)
    As(1,:)=[AAA(2,1) AAA(2,2) AAA(2,3) AAA(2,4) AAA(2,5) AAA(2,6)];
elseif Ass(1)>AAA(2,5) && Ass(1)<=AAA(3,5)
    As(1,:)=[AAA(3,1) AAA(3,2) AAA(3,3) AAA(3,4) AAA(3,5) AAA(3,6)];    
elseif Ass(1)>AAA(3,5) && Ass(1)<=AAA(4,5)
    As(1,:)=[AAA(4,1) AAA(4,2) AAA(4,3) AAA(4,4) AAA(4,5) AAA(4,6)];     
elseif Ass(1)>AAA(4,5) && Ass(1)<=AAA(5,5)
    As(1,:)=[AAA(5,1) AAA(5,2) AAA(5,3) AAA(5,4) AAA(5,5) AAA(5,6)];
elseif Ass(1)>AAA(5,5) && Ass(1)<=AAA(6,5)
    As(1,:)=[AAA(6,1) AAA(6,2) AAA(6,3) AAA(6,4) AAA(6,5) AAA(6,6)];    
elseif Ass(1)>AAA(6,5) && Ass(1)<=AAA(7,5)
    As(1,:)=[AAA(7,1) AAA(7,2) AAA(7,3) AAA(7,4) AAA(7,5) AAA(7,6)]; 
end
 
if Ass(2)<=AAA(1,5)
    As(2,:)=[AAA(1,1) AAA(1,2) AAA(1,3) AAA(1,4) AAA(1,5) AAA(1,6)];
elseif Ass(2)>AAA(1,5) && Ass(2)<=AAA(2,5)
    As(2,:)=[AAA(2,1) AAA(2,2) AAA(2,3) AAA(2,4) AAA(2,5) AAA(2,6)];
elseif Ass(2)>AAA(2,5) && Ass(2)<=AAA(3,5)
    As(2,:)=[AAA(3,1) AAA(3,2) AAA(3,3) AAA(3,4) AAA(3,5) AAA(3,6)];    
elseif Ass(2)>AAA(3,5) && Ass(2)<=AAA(4,5)
    As(2,:)=[AAA(4,1) AAA(4,2) AAA(4,3) AAA(4,4) AAA(4,5) AAA(4,6)];     
elseif Ass(2)>AAA(4,5) && Ass(2)<=AAA(5,5)
    As(2,:)=[AAA(5,1) AAA(5,2) AAA(5,3) AAA(5,4) AAA(5,5) AAA(5,6)];
elseif Ass(2)>AAA(5,5) && Ass(2)<=AAA(6,5)
    As(2,:)=[AAA(6,1) AAA(6,2) AAA(6,3) AAA(6,4) AAA(6,5) AAA(6,6)];    
elseif Ass(2)>AAA(6,5) && Ass(2)<=AAA(7,5)
    As(2,:)=[AAA(7,1) AAA(7,2) AAA(7,3) AAA(7,4) AAA(7,5) AAA(7,6)]; 
end        
  
if Ass(3)<=AAA(1,5)
    As(3,:)=[AAA(1,1) AAA(1,2) AAA(1,3) AAA(1,4) AAA(1,5) AAA(1,6)];
elseif Ass(3)>AAA(1,5) && Ass(3)<=AAA(2,5)
    As(3,:)=[AAA(2,1) AAA(2,2) AAA(2,3) AAA(2,4) AAA(2,5) AAA(2,6)];
elseif Ass(3)>AAA(2,5) && Ass(3)<=AAA(3,5)
    As(3,:)=[AAA(3,1) AAA(3,2) AAA(3,3) AAA(3,4) AAA(3,5) AAA(3,6)];    
elseif Ass(3)>AAA(3,5) && Ass(3)<=AAA(4,5)
    As(3,:)=[AAA(4,1) AAA(4,2) AAA(4,3) AAA(4,4) AAA(4,5) AAA(4,6)];     
elseif Ass(3)>AAA(4,5) && Ass(3)<=AAA(5,5)
    As(3,:)=[AAA(5,1) AAA(5,2) AAA(5,3) AAA(5,4) AAA(5,5) AAA(5,6)];
elseif Ass(3)>AAA(5,5) && Ass(3)<=AAA(6,5)
    As(3,:)=[AAA(6,1) AAA(6,2) AAA(6,3) AAA(6,4) AAA(6,5) AAA(6,6)];    
elseif Ass(3)>AAA(6,5) && Ass(3)<=AAA(7,5)
    As(3,:)=[AAA(7,1) AAA(7,2) AAA(7,3) AAA(7,4) AAA(7,5) AAA(7,6)]; 
end    
    
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%4层挡墙    
elseif n==4   
       Ass(1)=1000*h(1)*roumin;
       Ass(2)=1000*h(2)*roumin; 
       Ass(3)=1000*h(3)*roumin;
       Ass(4)=1000*h(4)*roumin;       
if Ass(1)<=AAA(1,5)
    As(1,:)=[AAA(1,1) AAA(1,2) AAA(1,3) AAA(1,4) AAA(1,5) AAA(1,6)];
elseif Ass(1)>AAA(1,5) && Ass(1)<=AAA(2,5)
    As(1,:)=[AAA(2,1) AAA(2,2) AAA(2,3) AAA(2,4) AAA(2,5) AAA(2,6)];
elseif Ass(1)>AAA(2,5) && Ass(1)<=AAA(3,5)
    As(1,:)=[AAA(3,1) AAA(3,2) AAA(3,3) AAA(3,4) AAA(3,5) AAA(3,6)];    
elseif Ass(1)>AAA(3,5) && Ass(1)<=AAA(4,5)
    As(1,:)=[AAA(4,1) AAA(4,2) AAA(4,3) AAA(4,4) AAA(4,5) AAA(4,6)];     
elseif Ass(1)>AAA(4,5) && Ass(1)<=AAA(5,5)
    As(1,:)=[AAA(5,1) AAA(5,2) AAA(5,3) AAA(5,4) AAA(5,5) AAA(5,6)];
elseif Ass(1)>AAA(5,5) && Ass(1)<=AAA(6,5)
    As(1,:)=[AAA(6,1) AAA(6,2) AAA(6,3) AAA(6,4) AAA(6,5) AAA(6,6)];    
elseif Ass(1)>AAA(6,5) && Ass(1)<=AAA(7,5)
    As(1,:)=[AAA(7,1) AAA(7,2) AAA(7,3) AAA(7,4) AAA(7,5) AAA(7,6)]; 
end
 
if Ass(2)<=AAA(1,5)
    As(2,:)=[AAA(1,1) AAA(1,2) AAA(1,3) AAA(1,4) AAA(1,5) AAA(1,6)];
elseif Ass(2)>AAA(1,5) && Ass(2)<=AAA(2,5)
    As(2,:)=[AAA(2,1) AAA(2,2) AAA(2,3) AAA(2,4) AAA(2,5) AAA(2,6)];
elseif Ass(2)>AAA(2,5) && Ass(2)<=AAA(3,5)
    As(2,:)=[AAA(3,1) AAA(3,2) AAA(3,3) AAA(3,4) AAA(3,5) AAA(3,6)];    
elseif Ass(2)>AAA(3,5) && Ass(2)<=AAA(4,5)
    As(2,:)=[AAA(4,1) AAA(4,2) AAA(4,3) AAA(4,4) AAA(4,5) AAA(4,6)];     
elseif Ass(2)>AAA(4,5) && Ass(2)<=AAA(5,5)
    As(2,:)=[AAA(5,1) AAA(5,2) AAA(5,3) AAA(5,4) AAA(5,5) AAA(5,6)];
elseif Ass(2)>AAA(5,5) && Ass(2)<=AAA(6,5)
    As(2,:)=[AAA(6,1) AAA(6,2) AAA(6,3) AAA(6,4) AAA(6,5) AAA(6,6)];    
elseif Ass(2)>AAA(6,5) && Ass(2)<=AAA(7,5)
    As(2,:)=[AAA(7,1) AAA(7,2) AAA(7,3) AAA(7,4) AAA(7,5) AAA(7,6)]; 
end        
  
if Ass(3)<=AAA(1,5)
    As(3,:)=[AAA(1,1) AAA(1,2) AAA(1,3) AAA(1,4) AAA(1,5) AAA(1,6)];
elseif Ass(3)>AAA(1,5) && Ass(3)<=AAA(2,5)
    As(3,:)=[AAA(2,1) AAA(2,2) AAA(2,3) AAA(2,4) AAA(2,5) AAA(2,6)];
elseif Ass(3)>AAA(2,5) && Ass(3)<=AAA(3,5)
    As(3,:)=[AAA(3,1) AAA(3,2) AAA(3,3) AAA(3,4) AAA(3,5) AAA(3,6)];    
elseif Ass(3)>AAA(3,5) && Ass(3)<=AAA(4,5)
    As(3,:)=[AAA(4,1) AAA(4,2) AAA(4,3) AAA(4,4) AAA(4,5) AAA(4,6)];     
elseif Ass(3)>AAA(4,5) && Ass(3)<=AAA(5,5)
    As(3,:)=[AAA(5,1) AAA(5,2) AAA(5,3) AAA(5,4) AAA(5,5) AAA(5,6)];
elseif Ass(3)>AAA(5,5) && Ass(3)<=AAA(6,5)
    As(3,:)=[AAA(6,1) AAA(6,2) AAA(6,3) AAA(6,4) AAA(6,5) AAA(6,6)];    
elseif Ass(3)>AAA(6,5) && Ass(3)<=AAA(7,5)
    As(3,:)=[AAA(7,1) AAA(7,2) AAA(7,3) AAA(7,4) AAA(7,5) AAA(7,6)]; 
end    
 
if Ass(4)<=AAA(1,5)
    As(4,:)=[AAA(1,1) AAA(1,2) AAA(1,3) AAA(1,4) AAA(1,5) AAA(1,6)];
elseif Ass(4)>AAA(1,5) && Ass(4)<=AAA(2,5)
    As(4,:)=[AAA(2,1) AAA(2,2) AAA(2,3) AAA(2,4) AAA(2,5) AAA(2,6)];
elseif Ass(4)>AAA(2,5) && Ass(4)<=AAA(3,5)
    As(4,:)=[AAA(3,1) AAA(3,2) AAA(3,3) AAA(3,4) AAA(3,5) AAA(3,6)];    
elseif Ass(4)>AAA(3,5) && Ass(4)<=AAA(4,5)
    As(4,:)=[AAA(4,1) AAA(4,2) AAA(4,3) AAA(4,4) AAA(4,5) AAA(4,6)];     
elseif Ass(4)>AAA(4,5) && Ass(4)<=AAA(5,5)
    As(4,:)=[AAA(5,1) AAA(5,2) AAA(5,3) AAA(5,4) AAA(5,5) AAA(5,6)];
elseif Ass(4)>AAA(5,5) && Ass(4)<=AAA(6,5)
    As(4,:)=[AAA(6,1) AAA(6,2) AAA(6,3) AAA(6,4) AAA(6,5) AAA(6,6)];    
elseif Ass(4)>AAA(6,5) && Ass(4)<=AAA(7,5)
    As(4,:)=[AAA(7,1) AAA(7,2) AAA(7,3) AAA(7,4) AAA(7,5) AAA(7,6)]; 
end    
    
end     
end

