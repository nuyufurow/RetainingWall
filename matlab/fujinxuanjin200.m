function [ A ] = fujinxuanjin200( n,As,ft,fy,h,M,cs,C,rg )
%本子函数用于负筋自动选筋，按照此子程序计算得到的配筋为最节省配筋
%本子函数即根据裂缝选筋
%n为挡墙层数
%As为peijinjisuan( M,cs,n,fy,fc,ft)中计算得到各点的计算配筋
% As=[A      AB      B       BC       C        CD        D       DE       E ] 
%   As(1)   As(2)   As(3)   As(4)   As(5)     As(6)     As(7)    As(8)   As(9)
% 受力钢筋最小直径为12mm，间距最大为150mm
roumin=max(0.002,0.45*ft/(100*fy));        %最小配筋率
if  n==1                                   %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%1层挡墙
    AsmaxA=As(1,1);                          %A点计算配筋
    
    if AsmaxA>4908 || AsmaxA<=0
       disp('A点选筋超出选筋库');
       A=[0 1000 0 1000 0 0;
          0 1000 0 1000 0 0]; 
       return      
    end    
    
    Astong=roumin*h(1)*1000;               %另通长筋=构造钢筋
    AssA=fujinshuchu200(AsmaxA,Astong);    %调用函数，输出A点的配筋，[直径 间距 直径 间距 实选面积]
    Asss=[AssA(1)  AssA(2)  AssA(3)  AssA(4)  AssA(5)];
                                           %B点配筋为负筋通长筋,裂缝不用验算

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A点的选筋
              d(1,:)=[Asss(1,1) fix(1000/Asss(1,2)) Asss(1,3) fix(1000/Asss(1,4))];%A点的配筋直径、数量
              w=liefeng1(M(1),cs,d(1,:),Asss(1,5),h(1),C,rg);                      %%%%%%计算A点裂缝
              if w<=0.2 
                 A1(1,:)=[Asss(1,1) Asss(1,2) Asss(1,3) Asss(1,4) Asss(1,5) w];%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A配筋输出格式统一为[直径 间距 直径 间距 实选面积]
              else
                  Asss0=Asss(1,5);                                         %Asss0用于做标记，判断后面增加面积是否超过1000mm2                  
                  Asss(1,5)= Asss(1,5)+10;                                %%%%%%%%%%%%%%裂缝计算不够时，实配钢筋面积增加10mm2,选筋后,再进行裂缝验算
                  
                  if Asss(1,5)>4908
                     disp('A点选筋超出选筋库');
                     A=[0 1000 0 1000 0 0;
                        0 1000 0 1000 0 0]; 
                     return      
                  end                  
                  
                  for j=1:200                                                %嵌套循环增加配筋面积，最多增加2000mm2 
                       AA=fujinshuchu200(Asss(1,5),Astong);                %%%%%%%%%%%%%%再次选筋
                       Asss(1,:)=[AA(1,1) AA(1,2) AA(1,3) AA(1,4) AA(1,5)];%将选筋结果填入Asss矩阵中
                       d(1,:)=[Asss(1,1) fix(1000/Asss(1,2)) Asss(1,3) fix(1000/Asss(1,4))]; 
                       w=liefeng1(M(1),cs,d,Asss(1,5),h,C,rg);                 
                      if w(1)<=0.2 
                         A1(1,:)=[Asss(1,1) Asss(1,2) Asss(1,3) Asss(1,4) Asss(1,5) w];%计算得到A点配筋和裂缝宽度
                         break
                      else
                         Asss(1,5)= Asss(1,5)+10;
                         
                        if Asss(1,5)>4908
                           disp('A点选筋超出选筋库');
                           A=[0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0]; 
                           return      
                        end                         
                         
                          if Asss(1,5)-Asss0>1000
                             disp('A点按裂缝选筋增加钢筋过大，请修改挡墙参数');
                             A=[0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0];                            
                             return                                        %结束本子程序
                          end
                      end
                  end
              end
              A=[A1(1,1) A1(1,2) A1(1,3) A1(1,4) A1(1,5)                        A1(1,6);
                 A1(1,1) A1(1,2) 0       0       pi*A1(1,1)^2*1000/(4*A1(1,2))  0 ];
            
elseif n==2         %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%2层挡墙
            AsmaxA=As(1,1);                                       %A点计算配筋
            AsmaxB=As(1,3);                                       %B点计算配筋
            
           if max(AsmaxA,AsmaxB)>4908 || min(AsmaxA,AsmaxB)<=0
              disp('A点选筋超出选筋库');
              A=[0 1000 0 1000 0 0;
                 0 1000 0 1000 0 0;
                 0 1000 0 1000 0 0]; 
              return      
           end             
            
            Astong=max(roumin*h(1)*1000,roumin*h(2)*1000);      %1层通长筋=构造钢筋，AB两点都用此值        
            AssA=fujinshuchu200(AsmaxA,Astong);                 %调用函数，输出A点的配筋，[直径 间距 直径 间距 实选面积]
            AssB=fujinshuchu200(AsmaxB,Astong);                 %调用函数，输出B点的配筋，[直径 间距 直径 间距 实选面积]
            Asss=[AssA(1)  AssA(2)  AssA(3)  AssA(4) AssA(5);
                  AssB(1)  AssB(2)  AssB(3)  AssB(4) AssB(5)];
                                                                %C点配筋为负筋通长筋,裂缝不用验算
                                                                
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A点配筋
              d(1,:)=[Asss(1,1) fix(1000/Asss(1,2)) Asss(1,3) fix(1000/Asss(1,4))];%A点的配筋直径、数量
              w=liefeng1(M(1),cs,d(1,:),Asss(1,5),h(1),C,rg);                      %%%%%%计算A点裂缝
              if w<=0.2 
                 A1(1,:)=[Asss(1,1) Asss(1,2) Asss(1,3) Asss(1,4) Asss(1,5) w];%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A配筋输出格式统一为[直径 间距 直径 间距 实选面积]
              else
                  Asss0=Asss(1,5);                                         %Asss0用于做标记，判断后面增加面积是否超过1000mm2                  
                  Asss(1,5)= Asss(1,5)+10;                                %%%%%%%%%%%%%%裂缝计算不够时，实配钢筋面积增加10mm2,选筋后,再进行裂缝验算
                  
                 if Asss(1,5)>4908
                    disp('A点选筋超出选筋库');
                    A=[0 1000 0 1000 0 0;
                       0 1000 0 1000 0 0;
                       0 1000 0 1000 0 0]; 
                    return      
                 end                   
                  
                  for j=1:200                                                %嵌套循环增加配筋面积，最多增加2000mm2 
                       AA=fujinshuchu200(Asss(1,5),Astong);                %%%%%%%%%%%%%%再次选筋
                       Asss(1,:)=[AA(1,1) AA(1,2) AA(1,3) AA(1,4) AA(1,5)];%将选筋结果填入Asss矩阵中
                       d(1,:)=[Asss(1,1) fix(1000/Asss(1,2)) Asss(1,3) fix(1000/Asss(1,4))]; 
                       w=liefeng1(M(1),cs,d(1,:),Asss(1,5),h(1),C,rg);                 
                      if w(1)<=0.2 
                         A1(1,:)=[Asss(1,1) Asss(1,2) Asss(1,3) Asss(1,4) Asss(1,5) w];%计算得到A点配筋和裂缝宽度
                         break
                      else
                         Asss(1,5)= Asss(1,5)+10;
                         
                       if Asss(1,5)>4908
                          disp('A点选筋超出选筋库');
                          A=[0 1000 0 1000 0 0;
                             0 1000 0 1000 0 0;
                             0 1000 0 1000 0 0]; 
                          return      
                       end                         
                         
                          if Asss(1,5)-Asss0>1000
                             disp('A点按裂缝选筋增加钢筋过大，请修改挡墙参数');
                             A=[0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0];                            
                             return                                        %结束本子程序
                          end
                      end
                  end
              end
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%B点配筋              
              d(2,:)=[Asss(2,1) fix(1000/Asss(2,2)) Asss(2,3) fix(1000/Asss(2,4))];%B点的配筋直径、数量
              wBl=liefeng1(M(3),cs,d(2,:),Asss(2,5),h(1),C,rg);
              wBr=liefeng1(M(3),cs,d(2,:),Asss(2,5),h(2),C,rg);                      
              w=max(wBl,wBr);                                              %%%%%%计算B点裂缝
              if w<=0.2 
                 A1(2,:)=[Asss(2,1) Asss(2,2) Asss(2,3) Asss(2,4) Asss(2,5) w];%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%配筋输出格式统一为[直径 间距 直径 间距 实选面积]
              else
                  Asss0=Asss(2,5);                                         %Asss0用于做标记，判断后面增加面积是否超过1000mm2                  
                  Asss(2,5)= Asss(2,5)+10;                                %%%%%%%%%%%%%%裂缝计算不够时，实配钢筋面积增加10mm2,选筋后,再进行裂缝验算
                  
                 if Asss(2,5)>4908
                    disp('B点选筋超出选筋库');
                    A=[0 1000 0 1000 0 0;
                       0 1000 0 1000 0 0;
                       0 1000 0 1000 0 0]; 
                    return      
                 end                  
                  
                  for j=1:200                                                %嵌套循环增加配筋面积，最多增加2000mm2 
                       AA=fujinshuchu200(Asss(2,5),Astong);                %%%%%%%%%%%%%%再次选筋
                       Asss(2,:)=[AA(1,1) AA(1,2) AA(1,3) AA(1,4) AA(1,5)];%将选筋结果填入Asss矩阵中
                       d(2,:)=[Asss(2,1) fix(1000/Asss(2,2)) Asss(2,3) fix(1000/Asss(2,4))]; 
                       wBl=liefeng1(M(3),cs,d(2,:),Asss(2,5),h(1),C,rg);
                       wBr=liefeng1(M(3),cs,d(2,:),Asss(2,5),h(2),C,rg);                      
                       w=max(wBl,wBr);                
                      if w<=0.2 
                         A1(2,:)=[Asss(2,1) Asss(2,2) Asss(2,3) Asss(2,4) Asss(2,5) w];%计算得到B点配筋和裂缝宽度
                         break
                      else
                         Asss(2,5)= Asss(2,5)+10;
                         
                       if Asss(2,5)>4908
                          disp('B点选筋超出选筋库');
                          A=[0 1000 0 1000 0 0;
                             0 1000 0 1000 0 0;
                             0 1000 0 1000 0 0]; 
                          return      
                       end                         
                         
                          if Asss(2,5)-Asss0>1000
                             disp('B点按裂缝选筋增加钢筋过大，请修改挡墙参数');
                             A=[0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0];                              
                             return                                        %结束本子程序
                          end
                      end
                  end
              end  
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A、B点通长筋取一样并调整受力筋级差             
        A=peijinxietiao200(A1,M,cs,h,C,rg);%%%%%调用子函数协调AB两点配筋
                               %%%%%电弧焊不用考虑通长筋级差，通长筋直径差可以不用控制
        A(3,:)=tongchang200( ft,fy,h(2));%%%%%%%%%将C点的通长筋加入矩阵中
             
        
elseif n==3         %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%3层挡墙
            AsmaxA=As(1,1);                                       %A点计算配筋
            AsmaxB=As(1,3);                                       %B点计算配筋
            AsmaxC=As(1,5);                                       %C点计算配筋 
            
            if max([AsmaxA AsmaxB AsmaxC])>4908 || min([AsmaxA AsmaxB AsmaxC])<=0
               disp('选筋超出选筋库');
               A=[0 1000 0 1000 0 0;
                  0 1000 0 1000 0 0;
                  0 1000 0 1000 0 0;
                  0 1000 0 1000 0 0]; 
               return      
            end            
            
            Astong1=max(roumin*h(1)*1000,roumin*h(2)*1000);               %1层通长筋=构造钢筋，AB两点都用此值
            Astong2=max(roumin*h(2)*1000,roumin*h(3)*1000);               %C点通长筋
            Astong=[Astong1 Astong1 Astong2];
            AssA=fujinshuchu200(AsmaxA,Astong(1));                 %调用函数，输出A点的配筋，[直径 间距 直径 间距 实选面积]
            AssB=fujinshuchu200(AsmaxB,Astong(2));                 %调用函数，输出B点的配筋，[直径 间距 直径 间距 实选面积]
            AssC=fujinshuchu200(AsmaxC,Astong(3));                 %调用函数，输出C点的配筋，[直径 间距 直径 间距 实选面积]            
            Asss=[AssA(1)  AssA(2)  AssA(3)  AssA(4) AssA(5);
                  AssB(1)  AssB(2)  AssB(3)  AssB(4) AssB(5);
                  AssC(1)  AssC(2)  AssC(3)  AssC(4) AssC(5)]; 
                                                                %D点配筋为负筋通长筋,裂缝不用验算
                                                                
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A点配筋
              d(1,:)=[Asss(1,1) fix(1000/Asss(1,2)) Asss(1,3) fix(1000/Asss(1,4))];%A点的配筋直径、数量
              w=liefeng1(M(1),cs,d(1,:),Asss(1,5),h(1),C,rg);                      %%%%%%计算A点裂缝
              if w<=0.2 
                 A1(1,:)=[Asss(1,1) Asss(1,2) Asss(1,3) Asss(1,4) Asss(1,5) w];%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A配筋输出格式统一为[直径 间距 直径 间距 实选面积 裂缝宽度]
              else
                  Asss0=Asss(1,5);                                         %Asss0用于做标记，判断后面增加面积是否超过1000mm2                  
                  Asss(1,5)= Asss(1,5)+10;                                %%%%%%%%%%%%%%裂缝计算不够时，实配钢筋面积增加10mm2,选筋后,再进行裂缝验算
                  
                  if Asss(1,5)>4908
                     disp('A点选筋超出选筋库');
                     A=[0 1000 0 1000 0 0;
                        0 1000 0 1000 0 0;
                        0 1000 0 1000 0 0;
                        0 1000 0 1000 0 0]; 
                     return      
                  end                  
                  
                  for j=1:200                                                %嵌套循环增加配筋面积，最多增加2000mm2 
                       AA=fujinshuchu200(Asss(1,5),Astong(1));                %%%%%%%%%%%%%%再次选筋
                       Asss(1,:)=[AA(1,1) AA(1,2) AA(1,3) AA(1,4) AA(1,5)];%将选筋结果填入Asss矩阵中
                       d(1,:)=[Asss(1,1) fix(1000/Asss(1,2)) Asss(1,3) fix(1000/Asss(1,4))]; 
                       w=liefeng1(M(1),cs,d(1,:),Asss(1,5),h(1),C,rg);                 
                      if w(1)<=0.2 
                         A1(1,:)=[Asss(1,1) Asss(1,2) Asss(1,3) Asss(1,4) Asss(1,5) w];
                         break
                      else
                         Asss(1,5)= Asss(1,5)+10;
                         
                        if Asss(1,5)>4908
                           disp('A点选筋超出选筋库');
                           A=[0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0]; 
                           return      
                        end                         
                         
                          if Asss(1,5)-Asss0>1000
                             disp('A点按裂缝选筋增加钢筋过大，请修改挡墙参数');
                             A=[0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0];                             
                             return                                         %结束本子程序
                          end
                      end
                  end
              end
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%B点配筋        
              d(2,:)=[Asss(2,1) fix(1000/Asss(2,2)) Asss(2,3) fix(1000/Asss(2,4))];%B点的配筋直径、数量
              wBl=liefeng1(M(3),cs,d(2,:),Asss(2,5),h(1),C,rg);
              wBr=liefeng1(M(3),cs,d(2,:),Asss(2,5),h(2),C,rg);                      
              w=max(wBl,wBr);                                              %%%%%%计算B点裂缝
              if w<=0.2 
                 A1(2,:)=[Asss(2,1) Asss(2,2) Asss(2,3) Asss(2,4) Asss(2,5) w];%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%配筋输出格式统一为[直径 间距 直径 间距 实选面积]
              else
                  Asss0=Asss(2,5);                                         %Asss0用于做标记，判断后面增加面积是否超过1000mm2                  
                  Asss(2,5)= Asss(2,5)+10;                                %%%%%%%%%%%%%%裂缝计算不够时，实配钢筋面积增加10mm2,选筋后,再进行裂缝验算
                  
                  if Asss(2,5)>4908
                     disp('B点选筋超出选筋库');
                     A=[0 1000 0 1000 0 0;
                        0 1000 0 1000 0 0;
                        0 1000 0 1000 0 0;
                        0 1000 0 1000 0 0]; 
                     return      
                  end                  
                  
                  for j=1:200                                                %嵌套循环增加配筋面积，最多增加2000mm2 
                       AA=fujinshuchu200(Asss(2,5),Astong(2));                %%%%%%%%%%%%%%再次选筋
                       Asss(2,:)=[AA(1,1) AA(1,2) AA(1,3) AA(1,4) AA(1,5)];%将选筋结果填入Asss矩阵中
                       d(2,:)=[Asss(2,1) fix(1000/Asss(2,2)) Asss(2,3) fix(1000/Asss(2,4))]; 
                       wBl=liefeng1(M(3),cs,d(2,:),Asss(2,5),h(1),C,rg);
                       wBr=liefeng1(M(3),cs,d(2,:),Asss(2,5),h(2),C,rg);                      
                       w=max(wBl,wBr);                
                      if w<=0.2 
                         A1(2,:)=[Asss(2,1) Asss(2,2) Asss(2,3) Asss(2,4) Asss(2,5) w];%计算得到B点配筋和裂缝宽度
                         break
                      else
                         Asss(2,5)= Asss(2,5)+10;
                         
                        if Asss(2,5)>4908
                           disp('B点选筋超出选筋库');
                           A=[0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0]; 
                           return      
                        end                          
                         
                          if Asss(2,5)-Asss0>1000
                             disp('B点按裂缝选筋增加钢筋过大，请修改挡墙参数');
                             A=[0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0];                              
                             return                                        %结束本子程序
                          end
                      end
                  end
              end               
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%C点配筋        
              d(3,:)=[Asss(3,1) fix(1000/Asss(3,2)) Asss(3,3) fix(1000/Asss(3,4))];%C点的配筋直径、数量
              wCl=liefeng1(M(5),cs,d(3,:),Asss(3,5),h(2),C,rg);
              wCr=liefeng1(M(5),cs,d(3,:),Asss(3,5),h(3),C,rg);                      
              w=max(wCl,wCr);                                              %%%%%%计算C点裂缝
              if w<=0.2 
                 A1(3,:)=[Asss(3,1) Asss(3,2) Asss(3,3) Asss(3,4) Asss(3,5) w];%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%配筋输出格式统一为[直径 间距 直径 间距 实选面积]
              else
                  Asss0=Asss(3,5);                                         %Asss0用于做标记，判断后面增加面积是否超过1000mm2                  
                  Asss(3,5)= Asss(3,5)+10;                                %%%%%%%%%%%%%%裂缝计算不够时，实配钢筋面积增加200mm2,选筋后,再进行裂缝验算
                  
                  if Asss(3,5)>4908
                     disp('C点选筋超出选筋库');
                     A=[0 1000 0 1000 0 0;
                        0 1000 0 1000 0 0;
                        0 1000 0 1000 0 0;
                        0 1000 0 1000 0 0]; 
                     return      
                  end                  
                  
                  for j=1:200                                                %嵌套循环增加配筋面积，最多增加2000mm2 
                       AA=fujinshuchu200(Asss(3,5),Astong(3));             %%%%%%%%%%%%%%再次选筋
                       Asss(3,:)=[AA(1,1) AA(1,2) AA(1,3) AA(1,4) AA(1,5)];%将选筋结果填入Asss矩阵中
                       d(3,:)=[Asss(3,1) fix(1000/Asss(3,2)) Asss(3,3) fix(1000/Asss(3,4))]; 
                       wCl=liefeng1(M(5),cs,d(3,:),Asss(3,5),h(2),C,rg);
                       wCr=liefeng1(M(5),cs,d(3,:),Asss(3,5),h(3),C,rg);                      
                       w=max(wCl,wCr);                
                      if w<=0.2 
                         A1(3,:)=[Asss(3,1) Asss(3,2) Asss(3,3) Asss(3,4) Asss(3,5) w];%计算得到C点配筋和裂缝宽度
                         break
                      else
                         Asss(3,5)= Asss(3,5)+10;
                         
                        if Asss(3,5)>4908
                           disp('C点选筋超出选筋库');
                           A=[0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0]; 
                           return      
                        end                         
                         
                          if Asss(3,5)-Asss0>1000
                             disp('C点按裂缝选筋增加钢筋过大，请修改挡墙参数');
                             A=[0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0];                             
                             return                                        %结束本子程序
                          end
                      end
                  end
              end               
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A、B点通长筋取一样并调整受力筋级差             
        A=peijinxietiao200(A1(1:2,:),M,cs,h,C,rg);%%%%%调用子函数协调AB两点配筋
                                      %%%%%电弧焊不用考虑通长筋级差，通长筋直径差可以不用控制
        A(3,:)=A1(3,:);                                      
        A(4,:)=tongchang200( ft,fy,h(3));%%%%%%%%%将C点的通长筋加入矩阵中              
              
  
elseif n==4         %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%4层挡墙
            AsmaxA=As(1,1);                                       %A点计算配筋
            AsmaxB=As(1,3);                                       %B点计算配筋
            AsmaxC=As(1,5);                                       %C点计算配筋
            AsmaxD=As(1,7);                                       %D点计算配筋
            
            if max([AsmaxA AsmaxB AsmaxC AsmaxD])>4908 || min([AsmaxA AsmaxB AsmaxC AsmaxD])<=0
               disp('选筋超出选筋库');
               A=[0 1000 0 1000 0 0;
                  0 1000 0 1000 0 0;
                  0 1000 0 1000 0 0;
                  0 1000 0 1000 0 0;
                  0 1000 0 1000 0 0]; 
               return      
            end            
            
            Astong1=max(roumin*h(1)*1000,roumin*h(2)*1000);               %1层通长筋=构造钢筋，AB两点都用此值
            Astong2=max(roumin*h(2)*1000,roumin*h(3)*1000);               %C点通长筋
            Astong3=max(roumin*h(3)*1000,roumin*h(4)*1000);
            Astong=[Astong1 Astong1 Astong2 Astong3];
            AssA=fujinshuchu200(AsmaxA,Astong(1));                 %调用函数，输出A点的配筋，[直径 间距 直径 间距 实选面积]
            AssB=fujinshuchu200(AsmaxB,Astong(2));                 %调用函数，输出B点的配筋，[直径 间距 直径 间距 实选面积]
            AssC=fujinshuchu200(AsmaxC,Astong(3));                 %调用函数，输出C点的配筋，[直径 间距 直径 间距 实选面积] 
            AssD=fujinshuchu200(AsmaxD,Astong(4));                 %调用函数，输出D点的配筋，[直径 间距 直径 间距 实选面积]
            Asss=[AssA(1)  AssA(2)  AssA(3)  AssA(4) AssA(5);
                  AssB(1)  AssB(2)  AssB(3)  AssB(4) AssB(5);
                  AssC(1)  AssC(2)  AssC(3)  AssC(4) AssC(5);
                  AssD(1)  AssD(2)  AssD(3)  AssD(4) AssD(5)]; 
                                                                %E点配筋为负筋通长筋,裂缝不用验算
                                                                                                                           
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A点配筋                                                                
              d(1,:)=[Asss(1,1) fix(1000/Asss(1,2)) Asss(1,3) fix(1000/Asss(1,4))];%A点的配筋直径、数量
              w=liefeng1(M(1),cs,d(1,:),Asss(1,5),h(1),C,rg);                      %%%%%%计算A点裂缝
              if w<=0.2 
                 A1(1,:)=[Asss(1,1) Asss(1,2) Asss(1,3) Asss(1,4) Asss(1,5) w];%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A配筋输出格式统一为[直径 间距 直径 间距 实选面积 裂缝]
              else
                  Asss0=Asss(1,5);                                         %Asss0用于做标记，判断后面增加面积是否超过1000mm2                  
                  Asss(1,5)= Asss(1,5)+10;                                %%%%%%%%%%%%%%裂缝计算不够时，实配钢筋面积增加10mm2,选筋后,再进行裂缝验算
                  
                  if Asss(1,5)>4908
                     disp('A点选筋超出选筋库');
                     A=[0 1000 0 1000 0 0;
                        0 1000 0 1000 0 0;
                        0 1000 0 1000 0 0;
                        0 1000 0 1000 0 0;
                        0 1000 0 1000 0 0]; 
                     return      
                  end                  
                  
                  for j=1:200                                                %嵌套循环增加配筋面积，最多增加2000mm2 
                       AA=fujinshuchu200(Asss(1,5),Astong(1));                %%%%%%%%%%%%%%再次选筋
                       Asss(1,:)=[AA(1,1) AA(1,2) AA(1,3) AA(1,4) AA(1,5)];%将选筋结果填入Asss矩阵中
                       d(1,:)=[Asss(1,1) fix(1000/Asss(1,2)) Asss(1,3) fix(1000/Asss(1,4))]; 
                       w=liefeng1(M(1),cs,d(1,:),Asss(1,5),h(1),C,rg);                 
                      if w(1)<=0.2 
                         A1(1,:)=[Asss(1,1) Asss(1,2) Asss(1,3) Asss(1,4) Asss(1,5) w];
                         break
                      else
                         Asss(1,5)= Asss(1,5)+10;
                         
                        if Asss(1,5)>4908
                           disp('A点选筋超出选筋库');
                           A=[0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0]; 
                           return      
                         end                         
                         
                          if Asss(1,5)-Asss0>1000
                             disp('A点按裂缝选筋增加钢筋过大，请修改挡墙参数');
                             A=[0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0];                            
                             return                                         %结束本子程序
                          end
                      end
                  end
              end
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%B点配筋        
              d(2,:)=[Asss(2,1) fix(1000/Asss(2,2)) Asss(2,3) fix(1000/Asss(2,4))];%B点的配筋直径、数量
              wBl=liefeng1(M(3),cs,d(2,:),Asss(2,5),h(1),C,rg);
              wBr=liefeng1(M(3),cs,d(2,:),Asss(2,5),h(2),C,rg);                      
              w=max(wBl,wBr);                                              %%%%%%计算B点裂缝
              if w<=0.2 
                 A1(2,:)=[Asss(2,1) Asss(2,2) Asss(2,3) Asss(2,4) Asss(2,5) w];%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%配筋输出格式统一为[直径 间距 直径 间距 实选面积]
              else
                  Asss0=Asss(2,5);                                         %Asss0用于做标记，判断后面增加面积是否超过1000mm2                  
                  Asss(2,5)= Asss(2,5)+10;                                %%%%%%%%%%%%%%裂缝计算不够时，实配钢筋面积增加10mm2,选筋后,再进行裂缝验算
                  
                  if Asss(2,5)>4908
                     disp('B点选筋超出选筋库');
                     A=[0 1000 0 1000 0 0;
                        0 1000 0 1000 0 0;
                        0 1000 0 1000 0 0;
                        0 1000 0 1000 0 0;
                        0 1000 0 1000 0 0]; 
                     return      
                  end                  
                  
                  for j=1:200                                                %嵌套循环增加配筋面积，最多增加2000mm2 
                       AA=fujinshuchu200(Asss(2,5),Astong(2));                %%%%%%%%%%%%%%再次选筋
                       Asss(2,:)=[AA(1,1) AA(1,2) AA(1,3) AA(1,4) AA(1,5)];%将选筋结果填入Asss矩阵中
                       d(2,:)=[Asss(2,1) fix(1000/Asss(2,2)) Asss(2,3) fix(1000/Asss(2,4))]; 
                       wBl=liefeng1(M(3),cs,d(2,:),Asss(2,5),h(1),C,rg);
                       wBr=liefeng1(M(3),cs,d(2,:),Asss(2,5),h(2),C,rg);                      
                       w=max(wBl,wBr);                
                      if w<=0.2 
                         A1(2,:)=[Asss(2,1) Asss(2,2) Asss(2,3) Asss(2,4) Asss(2,5) w];%计算得到B点配筋和裂缝宽度
                         break
                      else
                         Asss(2,5)= Asss(2,5)+10;
                         
                        if Asss(2,5)>4908
                           disp('B点选筋超出选筋库');
                           A=[0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0]; 
                           return      
                         end                         
                         
                          if Asss(2,5)-Asss0>1000
                             disp('B点按裂缝选筋增加钢筋过大，请修改挡墙参数');
                             A=[0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0];                             
                             return                                        %结束本子程序
                          end
                      end
                  end
              end                                                                            
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%C点配筋        
              d(3,:)=[Asss(3,1) fix(1000/Asss(3,2)) Asss(3,3) fix(1000/Asss(3,4))];%C点的配筋直径、数量
              wCl=liefeng1(M(5),cs,d(3,:),Asss(3,5),h(2),C,rg);
              wCr=liefeng1(M(5),cs,d(3,:),Asss(3,5),h(3),C,rg);                      
              w=max(wCl,wCr);                                              %%%%%%计算C点裂缝
              if w<=0.2 
                 A1(3,:)=[Asss(3,1) Asss(3,2) Asss(3,3) Asss(3,4) Asss(3,5) w];%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%配筋输出格式统一为[直径 间距 直径 间距 实选面积]
              else
                  Asss0=Asss(3,5);                                         %Asss0用于做标记，判断后面增加面积是否超过1000mm2                  
                  Asss(3,5)= Asss(3,5)+10;                                %%%%%%%%%%%%%%裂缝计算不够时，实配钢筋面积增加10mm2,选筋后,再进行裂缝验算
                  
                  if Asss(3,5)>4908
                     disp('C点选筋超出选筋库');
                     A=[0 1000 0 1000 0 0;
                        0 1000 0 1000 0 0;
                        0 1000 0 1000 0 0;
                        0 1000 0 1000 0 0;
                        0 1000 0 1000 0 0]; 
                     return      
                   end                  
                  
                  for j=1:200                                                %嵌套循环增加配筋面积，最多增加2000mm2 
                       AA=fujinshuchu200(Asss(3,5),Astong(3));                %%%%%%%%%%%%%%再次选筋
                       Asss(3,:)=[AA(1,1) AA(1,2) AA(1,3) AA(1,4) AA(1,5)];%将选筋结果填入Asss矩阵中
                       d(3,:)=[Asss(3,1) fix(1000/Asss(3,2)) Asss(3,3) fix(1000/Asss(3,4))]; 
                       wCl=liefeng1(M(5),cs,d(3,:),Asss(3,5),h(2),C,rg);
                       wCr=liefeng1(M(5),cs,d(3,:),Asss(3,5),h(3),C,rg);                      
                       w=max(wCl,wCr);                
                      if w<=0.2 
                         A1(3,:)=[Asss(3,1) Asss(3,2) Asss(3,3) Asss(3,4) Asss(3,5) w];%计算得到C点配筋和裂缝宽度
                         break
                      else
                         Asss(3,5)= Asss(3,5)+10;
                         
                        if Asss(3,5)>4908
                           disp('C点选筋超出选筋库');
                           A=[0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0]; 
                           return      
                        end                         
                         
                          if Asss(3,5)-Asss0>1000
                             disp('C点按裂缝选筋增加钢筋过大，请修改挡墙参数');
                             A=[0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0];                              
                             return                                        %结束本子程序
                          end
                      end
                  end
              end
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%D点配筋        
              d(4,:)=[Asss(4,1) fix(1000/Asss(4,2)) Asss(4,3) fix(1000/Asss(4,4))];%D点的配筋直径、数量
              wDl=liefeng1(M(7),cs,d(4,:),Asss(4,5),h(3),C,rg);
              wDr=liefeng1(M(7),cs,d(4,:),Asss(4,5),h(4),C,rg);                      
              w=max(wDl,wDr);                                              %%%%%%计算D点裂缝
              if w<=0.2 
                 A1(4,:)=[Asss(4,1) Asss(4,2) Asss(4,3) Asss(4,4) Asss(4,5) w];%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%配筋输出格式统一为[直径 间距 直径 间距 实选面积]
              else
                  Asss0=Asss(4,5);                                         %Asss0用于做标记，判断后面增加面积是否超过1000mm2                  
                  Asss(4,5)= Asss(4,5)+10;                                %%%%%%%%%%%%%%裂缝计算不够时，实配钢筋面积增加10mm2,选筋后,再进行裂缝验算
                  
                  if Asss(4,5)>4908
                     disp('D点选筋超出选筋库');
                     A=[0 1000 0 1000 0 0;
                        0 1000 0 1000 0 0;
                        0 1000 0 1000 0 0;
                        0 1000 0 1000 0 0;
                        0 1000 0 1000 0 0]; 
                     return      
                   end                  
                  
                  for j=1:200                                                %嵌套循环增加配筋面积，最多增加2000mm2 
                       AA=fujinshuchu200(Asss(4,5),Astong(4));                %%%%%%%%%%%%%%再次选筋
                       Asss(4,:)=[AA(1,1) AA(1,2) AA(1,3) AA(1,4) AA(1,5)];%将选筋结果填入Asss矩阵中
                       d(4,:)=[Asss(4,1) fix(1000/Asss(4,2)) Asss(4,3) fix(1000/Asss(4,4))]; 
                       wDl=liefeng1(M(7),cs,d(4,:),Asss(4,5),h(3),C,rg);
                       wDr=liefeng1(M(7),cs,d(4,:),Asss(4,5),h(4),C,rg);                      
                       w=max(wDl,wDr);                
                      if w<=0.2 
                         A1(4,:)=[Asss(4,1) Asss(4,2) Asss(4,3) Asss(4,4) Asss(4,5) w];%计算得到C点配筋和裂缝宽度
                         break
                      else
                         Asss(4,5)= Asss(4,5)+10;
                         
                        if Asss(4,5)>4908
                           disp('D点选筋超出选筋库');
                           A=[0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0]; 
                           return      
                        end                         
                         
                          if Asss(4,5)-Asss0>1000
                             disp('D点按裂缝选筋增加钢筋过大，请修改挡墙参数');
                             A=[0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0];                              
                             return                                        %结束本子程序
                          end
                      end
                  end
              end           
 %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A、B点通长筋取一样并调整受力筋级差             
        A=peijinxietiao200(A1(1:2,:),M,cs,h,C,rg);%%%%%调用子函数协调AB两点配筋
                                      %%%%%电弧焊不用考虑通长筋级差，通长筋直径差可以不用控制
        A(3,:)=A1(3,:);
        A(4,:)=A1(4,:);                                      
        A(5,:)=tongchang200( ft,fy,h(4));%%%%%%%%%将C点的通长筋加入矩阵中             
end
end



