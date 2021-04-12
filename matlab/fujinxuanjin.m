function [ A ] = fujinxuanjin( n,As,ft,fy,h,M,cs,C,rg )
%本子函数用于负筋自动选筋，按照此子程序计算得到的配筋为最节省配筋
%本子函数即根据裂缝选筋
%n为挡墙层数
%As为peijinjisuan( M,cs,n,fy,fc,ft)中计算得到各点的计算配筋
% As=[A      AB      B       BC       C        CD        D       DE       E ] 
%   As(1)   As(2)   As(3)   As(4)   As(5)     As(6)     As(7)    As(8)   As(9)
% 受力钢筋最小直径为12mm，间距最大为150mm
roumin=max(0.002,0.45*ft/(100*fy));        %最小配筋率
if  n==1                                   %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%1层挡墙
    AsmaxA=As(1);                          %A点计算配筋
    Astong=roumin*h(1);                    %另通长筋=构造钢筋
    AssA=fujinshuchu(AsmaxA,Astong);       %调用函数，输出A点的配筋，[直径 间距 直径 间距 实选面积]
    Asss=[AssA(1)  AssA(2)  AssA(3)  AssA(4)  AssA(5)];
                                           %B点配筋为负筋通长筋,裂缝不用验算

        for i=1                            %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A点的选筋
              d(i,:)=[Asss(i,1) fix(1000/Asss(i,2)) Asss(i,3) fix(1000/Asss(i,4))];%i点的配筋直径、数量
              w=liefeng1(M(i),cs,d,Asss(i,5),h,C,rg);                      %%%%%%计算i点裂缝
              if w<=0.2 
                 A(i,:)=[Asss(i,1) Asss(i,2) Asss(i,3) Asss(i,4) Asss(i,5)];%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A配筋输出格式统一为[直径 间距 直径 间距 实选面积]
              else
                  Asss0=Asss(i,5);                                         %Asss0用于做标记，判断后面增加面积是否超过1000mm2                  
                  Asss(i,5)= Asss(i,5)+200;                                %%%%%%%%%%%%%%裂缝计算不够时，实配钢筋面积增加200mm2,选筋后,再进行裂缝验算
                  for j=1:5                                                %嵌套循环增加配筋面积，最多增加1000mm2 
                       AA=fujinshuchu(Asss(i,5),Astong);                   %%%%%%%%%%%%%%再次选筋
                       Asss(i,:)=[AA(i,1) AA(i,2) AA(i,3) AA(i,4) AA(i,5)];%将选筋结果填入Asss矩阵中
                       d(i,:)=[Asss(i,1) fix(1000/Asss(i,2)) Asss(i,3) fix(1000/Asss(i,4))]; 
                       w=liefeng1(M(i),cs,d,Asss(i,5),h,C,rg);                 
                      if w(i)<=0.2 
                         A(i,:)=[Asss(i,1) Asss(i,2) Asss(i,3) Asss(i,4) Asss(i,5)];
                         break
                      else
                         Asss(i,5)= Asss(i,5)+200;
                          if Asss(i,5)-Asss0>1000
                             disp('按裂缝选筋增加钢筋过大，请修改挡墙参数');
                             return                                         %结束本子程序
                          end
                      end
                  end
              end
        end        
      
elseif n==2         %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%2层挡墙
            AsmaxA=As(1);                                       %A点计算配筋
            AsmaxB=As(3);                                       %B点计算配筋
            Astong=max(roumin*h(1),roumin*h(2));                %1层通长筋=构造钢筋，AB两点都用此值        
            AssA=fujinshuchu(AsmaxA,Astong);                   %调用函数，输出A点的配筋，[直径 间距 直径 间距 实选面积]
            AssB=fujinshuchu(AsmaxB,Astong);      %调用函数，输出B点的配筋，[直径 间距 直径 间距 实选面积]           
            Asss=[AssA(1)  AssA(2)  AssA(3)  AssA(4) AssA(5);
                  AssB(1)  AssB(2)  AssB(3)  AssB(4) AssB(5)];
                                                                %C点配筋为负筋通长筋,裂缝不用验算
                                                                
        for i=1:2                                               %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A点和B点的选筋
              d(i,:)=[Asss(i,1) fix(1000/Asss(i,2)) Asss(i,3) fix(1000/Asss(i,4))];%i点的配筋直径、数量
              w=liefeng1(M(i),cs,d,Asss(i,5),h,C,rg);                      %%%%%%计算i点裂缝
              if w<=0.2 
                 A(i,:)=[Asss(i,1) Asss(i,2) Asss(i,3) Asss(i,4) Asss(i,5)];%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A配筋输出格式统一为[直径 间距 直径 间距 实选面积]
              else
                  Asss0=Asss(i,5);                                         %Asss0用于做标记，判断后面增加面积是否超过1000mm2                  
                  Asss(i,5)= Asss(i,5)+200;                                %%%%%%%%%%%%%%裂缝计算不够时，实配钢筋面积增加200mm2,选筋后,再进行裂缝验算
                  for j=1:5                                                %嵌套循环增加配筋面积，最多增加1000mm2 
                       AA=fujinshuchu(Asss(i,5),Astong);                   %%%%%%%%%%%%%%再次选筋
                       Asss(i,:)=[AA(i,1) AA(i,2) AA(i,3) AA(i,4) AA(i,5)];%将选筋结果填入Asss矩阵中
                       d(i,:)=[Asss(i,1) fix(1000/Asss(i,2)) Asss(i,3) fix(1000/Asss(i,4))]; 
                       w=liefeng1(M(i),cs,d,Asss(i,5),h,C,rg);                 
                      if w(i)<=0.2 
                         A(i,:)=[Asss(i,1) Asss(i,2) Asss(i,3) Asss(i,4) Asss(i,5)];
                         break
                      else
                         Asss(i,5)= Asss(i,5)+200;
                          if Asss(i,5)-Asss0>1000
                             disp('按裂缝选筋增加钢筋过大，请修改挡墙参数');
                             return                                         %结束本子程序
                          end
                      end
                  end
              end
        end                                                                
                                                                   
elseif n==3         %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%3层挡墙
            AsmaxA=As(1);                                       %A点计算配筋
            AsmaxB=As(3);                                       %B点计算配筋
            AsmaxC=As(5);                                       %C点计算配筋              
            Astong1=max(roumin*h(1),roumin*h(2));               %1层通长筋=构造钢筋，AB两点都用此值
            Astong2=max(roumin*h(2),roumin*h(3));               %C点通长筋
            Astong=[Astong1 Astong1 Astong2];
            AssA=fujinshuchu(AsmaxA,Astong(1));                 %调用函数，输出A点的配筋，[直径 间距 直径 间距 实选面积]
            AssB=fujinshuchu(AsmaxB,Astong(2));                 %调用函数，输出B点的配筋，[直径 间距 直径 间距 实选面积]
            AssC=fujinshuchu(AsmaxC,Astong(3));                 %调用函数，输出C点的配筋，[直径 间距 直径 间距 实选面积]            
            Asss=[AssA(1)  AssA(2)  AssA(3)  AssA(4) AssA(5);
                  AssB(1)  AssB(2)  AssB(3)  AssB(4) AssB(5);
                  AssC(1)  AssC(2)  AssC(3)  AssC(4) AssC(5)]; 
                                                                %D点配筋为负筋通长筋,裂缝不用验算
                                                                
        for i=1:3                                               %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A、B、C点的选筋
              d(i,:)=[Asss(i,1) fix(1000/Asss(i,2)) Asss(i,3) fix(1000/Asss(i,4))];%i点的配筋直径、数量
              w=liefeng1(M(i),cs,d,Asss(i,5),h,C,rg);                      %%%%%%计算i点裂缝
              if w<=0.2 
                 A(i,:)=[Asss(i,1) Asss(i,2) Asss(i,3) Asss(i,4) Asss(i,5)];%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A配筋输出格式统一为[直径 间距 直径 间距 实选面积]
              else
                  Asss0=Asss(i,5);                                         %Asss0用于做标记，判断后面增加面积是否超过1000mm2                  
                  Asss(i,5)= Asss(i,5)+200;                                %%%%%%%%%%%%%%裂缝计算不够时，实配钢筋面积增加200mm2,选筋后,再进行裂缝验算
                  for j=1:5                                                %嵌套循环增加配筋面积，最多增加1000mm2 
                       AA=fujinshuchu(Asss(i,5),Astong(i));                %%%%%%%%%%%%%%再次选筋
                       Asss(i,:)=[AA(i,1) AA(i,2) AA(i,3) AA(i,4) AA(i,5)];%将选筋结果填入Asss矩阵中
                       d(i,:)=[Asss(i,1) fix(1000/Asss(i,2)) Asss(i,3) fix(1000/Asss(i,4))]; 
                       w=liefeng1(M(i),cs,d,Asss(i,5),h,C,rg);                 
                      if w(i)<=0.2 
                         A(i,:)=[Asss(i,1) Asss(i,2) Asss(i,3) Asss(i,4) Asss(i,5)];
                         break
                      else
                         Asss(i,5)= Asss(i,5)+200;
                          if Asss(i,5)-Asss0>1000
                             disp('按裂缝选筋增加钢筋过大，请修改挡墙参数');
                             return                                         %结束本子程序
                          end
                      end
                  end
              end
        end  
   
elseif n==4         %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%4层挡墙
            AsmaxA=As(1);                                       %A点计算配筋
            AsmaxB=As(3);                                       %B点计算配筋
            AsmaxC=As(5);                                       %C点计算配筋
            AsmaxD=As(7);                                       %D点计算配筋
            Astong1=max(roumin*h(1),roumin*h(2));               %1层通长筋=构造钢筋，AB两点都用此值
            Astong2=max(roumin*h(2),roumin*h(3));               %C点通长筋
            Astong3=max(roumin*h(3),roumin*h(4));
            Astong=[Astong1 Astong1 Astong2 Astong3];
            AssA=fujinshuchu(AsmaxA,Astong(1));                 %调用函数，输出A点的配筋，[直径 间距 直径 间距 实选面积]
            AssB=fujinshuchu(AsmaxB,Astong(2));                 %调用函数，输出B点的配筋，[直径 间距 直径 间距 实选面积]
            AssC=fujinshuchu(AsmaxC,Astong(3));                 %调用函数，输出C点的配筋，[直径 间距 直径 间距 实选面积] 
            AssD=fujinshuchu(AsmaxD,Astong(4));                 %调用函数，输出D点的配筋，[直径 间距 直径 间距 实选面积]
            Asss=[AssA(1)  AssA(2)  AssA(3)  AssA(4) AssA(5);
                  AssB(1)  AssB(2)  AssB(3)  AssB(4) AssB(5);
                  AssC(1)  AssC(2)  AssC(3)  AssC(4) AssC(5);
                  AssD(1)  AssD(2)  AssD(3)  AssD(4) AssD(5)]; 
                                                                %D点配筋为负筋通长筋,裂缝不用验算
                                                                
        for i=1:4                                               %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A、B、C、D点的选筋
              d(i,:)=[Asss(i,1) fix(1000/Asss(i,2)) Asss(i,3) fix(1000/Asss(i,4))];%i点的配筋直径、数量
              w=liefeng1(M(i),cs,d,Asss(i,5),h,C,rg);                      %%%%%%计算i点裂缝
              if w<=0.2 
                 A(i,:)=[Asss(i,1) Asss(i,2) Asss(i,3) Asss(i,4) Asss(i,5)];%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A配筋输出格式统一为[直径 间距 直径 间距 实选面积]
              else
                  Asss0=Asss(i,5);                                         %Asss0用于做标记，判断后面增加面积是否超过1000mm2                  
                  Asss(i,5)= Asss(i,5)+200;                                %%%%%%%%%%%%%%裂缝计算不够时，实配钢筋面积增加200mm2,选筋后,再进行裂缝验算
                  for j=1:5                                                %嵌套循环增加配筋面积，最多增加1000mm2 
                       AA=fujinshuchu(Asss(i,5),Astong(i));                %%%%%%%%%%%%%%再次选筋
                       Asss(i,:)=[AA(i,1) AA(i,2) AA(i,3) AA(i,4) AA(i,5)];%将选筋结果填入Asss矩阵中
                       d(i,:)=[Asss(i,1) fix(1000/Asss(i,2)) Asss(i,3) fix(1000/Asss(i,4))]; 
                       w=liefeng1(M(i),cs,d,Asss(i,5),h,C,rg);                 
                      if w(i)<=0.2 
                         A(i,:)=[Asss(i,1) Asss(i,2) Asss(i,3) Asss(i,4) Asss(i,5)];
                         break
                      else
                         Asss(i,5)= Asss(i,5)+200;
                          if Asss(i,5)-Asss0>1000
                             disp('按裂缝选筋增加钢筋过大，请修改挡墙参数');
                             return                                         %结束本子程序
                          end
                      end
                  end
              end
        end                                                                

end
end

