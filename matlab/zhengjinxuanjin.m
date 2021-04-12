function [ A ] =zhengjinxuanjin( n,As,h,M,cs,C,rg )
%本子函数用于正筋选筋
%本子函数即根据裂缝选筋
%n为挡墙层数
%As为peijinjisuan( M,cs,n,fy,fc,ft)中计算得到各点的计算配筋
% As=[A      AB      B       BC       C        CD        D       DE       E ] 
%   As(1)   As(2)   As(3)   As(4)   As(5)     As(6)     As(7)    As(8)   As(9)
% 受力钢筋最小直径为12mm，间距最大为150mm
%peijinjisuan中已经考虑最小配筋率
%M为neilizuhe(n,MM,Mmax)求得的各节点和跨中的配筋用弯矩
if  n==1                                   %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%1层挡墙
    AsmaxAB=As(1,2);                         %AB跨中计算配筋
   
    if AsmaxAB>4909 || AsmaxAB<=0   %超出选筋库时或之前配筋计算超筋时令其配筋为0
        disp('选筋超出选筋库');
        A=[0 1000 0 1000 0 0]; 
        return      
    end
    
    AssAB=zhengjinshuchu(AsmaxAB);         %调用函数，输出AB点的配筋，[直径 间距 实选面积]
    Asss=[AssAB(1)  AssAB(2)  0  0  AssAB(3)];%与负筋统一格式

                                  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%AB跨中选筋
              d(1,:)=[Asss(1,1) fix(1000/Asss(1,2)) 0 0];%i点的配筋直径、数量
              w=liefeng1(M(1*2),cs,d(1,:),Asss(1,5),h(1),C,rg);                      %%%%%%计算i跨裂缝
              if w(1)<=0.2 
                 A(1,:)=[Asss(1,1) Asss(1,2) Asss(1,3) Asss(1,4) Asss(1,5) w(1)];%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A配筋输出格式统一为[直径 间距 直径 间距 实选面积]
              else
                  Asss0=Asss(1,5);                                         %Asss0用于做标记，判断后面增加面积是否超过1000mm2                  
                  Asss(1,5)= Asss(1,5)+1;                                %%%%%%%%%%%%%%裂缝计算不够时，实配钢筋面积增加10mm2,选筋后,再进行裂缝验算
                    
                  if Asss(1,5)>4909       %超出选筋库时令其配筋为0
                     disp('选筋超出选筋库');
                     A=[0 1000 0 1000 0 0]; 
                     return      
                  end
                  
                  for j=1:2000,                                                %嵌套循环增加配筋面积，最多增加2000mm2 
                       AA=zhengjinshuchu(Asss(1,5));                %%%%%%%%%%%%%%再次选筋
                       Asss(1,:)=[AA(1,1) AA(1,2)  0  0  AA(1,3)];%将选筋结果填入Asss矩阵中
                       d(1,:)=[Asss(1,1) fix(1000/Asss(1,2)) 0 0]; 
                       w=liefeng1(M(1*2),cs,d(1,:),Asss(1,5),h(1),C,rg);                 
                      if w(1)<=0.2 
                         A(1,:)=[Asss(1,1) Asss(1,2) Asss(1,3) Asss(1,4) Asss(1,5) w(1)];
                         break
                      else
                         Asss(1,5)= Asss(1,5)+1;
                         
                          if Asss(1,5)>4909       %超出选筋库时令其配筋为0
                             disp('选筋超出选筋库');
                             A(1,:)=[0 1000 0 1000 0 0]; 
                             return      
                          end
                          
                          if Asss(1,5)-Asss0>1000
                             disp('按裂缝选筋增加钢筋过大，请修改挡墙参数');
                             A(1,:)=[0 1000 0 1000 0 0];   
                             return                                         %结束本子程序
                          end
                      end
                  end
              end
          

elseif  n==2                               %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%2层挡墙
    AsmaxAB=As(1,2);                         %AB跨中计算配筋
    AsmaxBC=As(1,4);                         %BC跨中计算配筋 
   
        if max(AsmaxAB,AsmaxBC)>4909 || min(AsmaxAB,AsmaxBC)<=0      %超出选筋库时或配筋计算超筋时令其配筋为0
           disp('选筋超出选筋库');
           A=[0 1000 0 1000 0 0;
              0 1000 0 1000 0 0]; 
           return      
        end
   
    AssAB=zhengjinshuchu(AsmaxAB);         %调用函数，输出AB点的配筋，[直径 间距 实选面积]
    AssBC=zhengjinshuchu(AsmaxBC);         %调用函数，输出BC点的配筋，[直径 间距 实选面积]
    Asss=[AssAB(1)  AssAB(2)  0  0  AssAB(3);
          AssBC(1)  AssBC(2)  0  0  AssBC(3)];%与负筋统一格式[直径 间距 直径 间距 实选面积]

 %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%跨中选筋     
for i=1:2
              d(i,:)=[Asss(i,1) fix(1000/Asss(i,2)) 0 0];%i层配筋直径、数量
              w=liefeng1(M(2*i),cs,d(i,:),Asss(i,5),h(i),C,rg);                      %%%%%%计算i跨裂缝
              if w(1)<=0.2 
                 A(i,:)=[Asss(i,1) Asss(i,2) Asss(i,3) Asss(i,4) Asss(i,5) w(1)];%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A配筋输出格式统一为[直径 间距 直径 间距 实选面积]
              else
                  Asss0=Asss(i,5);                                         %Asss0用于做标记，判断后面增加面积是否超过1000mm2                  
                  Asss(i,5)= Asss(i,5)+1;                                %%%%%%%%%%%%%%裂缝计算不够时，实配钢筋面积增加200mm2,选筋后,再进行裂缝验算
                  
                 if Asss(i,5)>4909       %超出选筋库时令其配筋为0
                    disp('选筋超出选筋库');
                    A=[0 1000 0 1000 0 0;
                       0 1000 0 1000 0 0]; 
                    return      
                 end                  
                                    
                  for j=1:2000,                                                %嵌套循环增加配筋面积，最多增加2000mm2 
                       AA=zhengjinshuchu(Asss(i,5));                %%%%%%%%%%%%%%再次选筋
                       Asss(i,:)=[AA(1,1) AA(1,2)  0  0  AA(1,3)];%将选筋结果填入Asss矩阵中
                       d(i,:)=[Asss(i,1) fix(1000/Asss(i,2)) 0 0]; 
                       w=liefeng1(M(2*i),cs,d(i,:),Asss(i,5),h(i),C,rg);                 
                      if w(1)<=0.2 
                         A(i,:)=[Asss(i,1) Asss(i,2) Asss(i,3) Asss(i,4) Asss(i,5) w(1)];
                         break
                      else
                         Asss(i,5)= Asss(i,5)+1;
                         
                        if Asss(i,5)>4909       %超出选筋库时令其配筋为0
                           disp('选筋超出选筋库');
                           A=[0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0]; 
                           return      
                        end                         
                         
                          if Asss(i,5)-Asss0>1000
                             disp('按裂缝选筋增加钢筋过大，请修改挡墙参数');
                             A=[0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0];
                             return                                         %结束本子程序
                          end
                      end
                  end
              end             
end 
              
              
              
elseif  n==3                                   %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%3层挡墙
    AsmaxAB=As(1,2);                         %AB跨中计算配筋
    AsmaxBC=As(1,4);                         %BC跨中计算配筋 
    AsmaxCD=As(1,6);                         %CD跨中计算配筋  
    
        if max([AsmaxAB AsmaxBC AsmaxCD])>4909 || min([AsmaxAB AsmaxBC AsmaxCD])<=0      %超出选筋库时或配筋计算超筋时令其配筋为0
           disp('选筋超出选筋库');
           A=[0 1000 0 1000 0 0;
              0 1000 0 1000 0 0;
              0 1000 0 1000 0 0]; 
           return      
        end
    
    AssAB=zhengjinshuchu(AsmaxAB);         %调用函数，输出AB点的配筋，[直径 间距 实选面积]
    AssBC=zhengjinshuchu(AsmaxBC);         %调用函数，输出BC点的配筋，[直径 间距 实选面积]
    AssCD=zhengjinshuchu(AsmaxCD);         %调用函数，输出CD点的配筋，[直径 间距 实选面积]    
    Asss=[AssAB(1)  AssAB(2)  0  0  AssAB(3);
          AssBC(1)  AssBC(2)  0  0  AssBC(3);
          AssCD(1)  AssCD(2)  0  0  AssCD(3)];%与负筋统一格式[直径 间距 直径 间距 实选面积]
      
 %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%跨中选筋     
for i=1:3
              d(i,:)=[Asss(i,1) fix(1000/Asss(i,2)) 0 0];%i层配筋直径、数量
              w=liefeng1(M(2*i),cs,d(i,:),Asss(i,5),h(i),C,rg);                      %%%%%%计算i跨裂缝
              if w(1)<=0.2 
                 A(i,:)=[Asss(i,1) Asss(i,2) Asss(i,3) Asss(i,4) Asss(i,5) w(1)];%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A配筋输出格式统一为[直径 间距 直径 间距 实选面积]
              else
                  Asss0=Asss(i,5);                                         %Asss0用于做标记，判断后面增加面积是否超过1000mm2                  
                  Asss(i,5)= Asss(i,5)+1;                                %%%%%%%%%%%%%%裂缝计算不够时，实配钢筋面积增加200mm2,选筋后,再进行裂缝验算
                  
                 if Asss(i,5)>4909       %超出选筋库时令其配筋为0
                    disp('选筋超出选筋库');
                    A=[0 1000 0 1000 0 0;
                       0 1000 0 1000 0 0;
                       0 1000 0 1000 0 0]; 
                    return      
                 end                  
                                    
                  for j=1:2000,                                                %嵌套循环增加配筋面积，最多增加2000mm2 
                       AA=zhengjinshuchu(Asss(i,5));                %%%%%%%%%%%%%%再次选筋
                       Asss(i,:)=[AA(1,1) AA(1,2)  0  0  AA(1,3)];%将选筋结果填入Asss矩阵中
                       d(i,:)=[Asss(i,1) fix(1000/Asss(i,2)) 0 0]; 
                       w=liefeng1(M(2*i),cs,d(i,:),Asss(i,5),h(i),C,rg);                 
                      if w(1)<=0.2 
                         A(i,:)=[Asss(i,1) Asss(i,2) Asss(i,3) Asss(i,4) Asss(i,5) w(1)];
                         break
                      else
                         Asss(i,5)= Asss(i,5)+1;
                         
                        if Asss(i,5)>4909       %超出选筋库时令其配筋为0
                           disp('选筋超出选筋库');
                           A=[0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0]; 
                           return      
                        end                         
                         
                          if Asss(i,5)-Asss0>1000
                             disp('按裂缝选筋增加钢筋过大，请修改挡墙参数');
                             A=[0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0];
                             return                                         %结束本子程序
                          end
                      end
                  end
              end             
end              
              
elseif  n==4                                   %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%4层挡墙
    AsmaxAB=As(1,2);                         %AB跨中计算配筋
    AsmaxBC=As(1,4);                         %BC跨中计算配筋 
    AsmaxCD=As(1,6);                         %CD跨中计算配筋
    AsmaxDE=As(1,8);                         %DE跨中计算配筋  
    
        if max([AsmaxAB AsmaxBC AsmaxCD AsmaxDE])>4909 || min([AsmaxAB AsmaxBC AsmaxCD AsmaxDE])<=0     %超出选筋库时或配筋计算超筋时令其配筋为0
           disp('选筋超出选筋库');
           A=[0 1000 0 1000 0 0;
              0 1000 0 1000 0 0;
              0 1000 0 1000 0 0;
              0 1000 0 1000 0 0]; 
           return      
        end  
    
    AssAB=zhengjinshuchu(AsmaxAB);         %调用函数，输出AB点的配筋，[直径 间距 实选面积]
    AssBC=zhengjinshuchu(AsmaxBC);         %调用函数，输出BC点的配筋，[直径 间距 实选面积]
    AssCD=zhengjinshuchu(AsmaxCD);         %调用函数，输出CD点的配筋，[直径 间距 实选面积]
    AssDE=zhengjinshuchu(AsmaxDE);         %调用函数，输出CD点的配筋，[直径 间距 实选面积]    
    Asss=[AssAB(1)  AssAB(2)  0  0  AssAB(3);
          AssBC(1)  AssBC(2)  0  0  AssBC(3);
          AssCD(1)  AssCD(2)  0  0  AssCD(3);
          AssDE(1)  AssDE(2)  0  0  AssDE(3)];%与负筋统一格式[直径 间距 直径 间距 实选面积]

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%跨中选筋
for i=1:4
              d(i,:)=[Asss(i,1) fix(1000/Asss(i,2)) 0 0];%i层配筋直径、数量
              w=liefeng1(M(2*i),cs,d(i,:),Asss(i,5),h(i),C,rg);                      %%%%%%计算i跨裂缝
              if w(1)<=0.2 
                 A(i,:)=[Asss(i,1) Asss(i,2) Asss(i,3) Asss(i,4) Asss(i,5) w(1)];%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A配筋输出格式统一为[直径 间距 直径 间距 实选面积]
              else
                  Asss0=Asss(i,5);                                         %Asss0用于做标记，判断后面增加面积是否超过1000mm2                  
                  Asss(i,5)= Asss(i,5)+1;                                %%%%%%%%%%%%%%裂缝计算不够时，实配钢筋面积增加200mm2,选筋后,再进行裂缝验算
                  
                 if Asss(i,5)>4909       %超出选筋库时令其配筋为0
                    disp('选筋超出选筋库');
                    A=[0 1000 0 1000 0 0;
                       0 1000 0 1000 0 0;
                       0 1000 0 1000 0 0;
                       0 1000 0 1000 0 0]; 
                    return      
                 end                  
                                    
                  for j=1:2000,                                                %嵌套循环增加配筋面积，最多增加2000mm2 
                       AA=zhengjinshuchu(Asss(i,5));                %%%%%%%%%%%%%%再次选筋
                       Asss(i,:)=[AA(1,1) AA(1,2)  0  0  AA(1,3)];%将选筋结果填入Asss矩阵中
                       d(i,:)=[Asss(i,1) fix(1000/Asss(i,2)) 0 0]; 
                       w=liefeng1(M(2*i),cs,d(i,:),Asss(i,5),h(i),C,rg);                 
                      if w(1)<=0.2 
                         A(i,:)=[Asss(i,1) Asss(i,2) Asss(i,3) Asss(i,4) Asss(i,5) w(1)];
                         break
                      else
                         Asss(i,5)= Asss(i,5)+1;
                         
                        if Asss(i,5)>4909       %超出选筋库时令其配筋为0
                           disp('选筋超出选筋库');
                           A=[0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0;
                              0 1000 0 1000 0 0]; 
                           return      
                        end                         
                         
                          if Asss(i,5)-Asss0>1000
                             disp('按裂缝选筋增加钢筋过大，请修改挡墙参数');
                             A=[0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0;
                                0 1000 0 1000 0 0];
                             return                                         %结束本子程序
                          end
                      end
                  end
              end
end                           
end
end

