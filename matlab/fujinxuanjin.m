function [ A ] = fujinxuanjin( n,As,ft,fy,h,M,cs,C,rg )
%���Ӻ������ڸ����Զ�ѡ����մ��ӳ������õ������Ϊ���ʡ���
%���Ӻ����������ѷ�ѡ��
%nΪ��ǽ����
%AsΪpeijinjisuan( M,cs,n,fy,fc,ft)�м���õ�����ļ������
% As=[A      AB      B       BC       C        CD        D       DE       E ] 
%   As(1)   As(2)   As(3)   As(4)   As(5)     As(6)     As(7)    As(8)   As(9)
% �����ֽ���Сֱ��Ϊ12mm��������Ϊ150mm
roumin=max(0.002,0.45*ft/(100*fy));        %��С�����
if  n==1                                   %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%1�㵲ǽ
    AsmaxA=As(1);                          %A��������
    Astong=roumin*h(1);                    %��ͨ����=����ֽ�
    AssA=fujinshuchu(AsmaxA,Astong);       %���ú��������A�����[ֱ�� ��� ֱ�� ��� ʵѡ���]
    Asss=[AssA(1)  AssA(2)  AssA(3)  AssA(4)  AssA(5)];
                                           %B�����Ϊ����ͨ����,�ѷ첻������

        for i=1                            %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A���ѡ��
              d(i,:)=[Asss(i,1) fix(1000/Asss(i,2)) Asss(i,3) fix(1000/Asss(i,4))];%i������ֱ��������
              w=liefeng1(M(i),cs,d,Asss(i,5),h,C,rg);                      %%%%%%����i���ѷ�
              if w<=0.2 
                 A(i,:)=[Asss(i,1) Asss(i,2) Asss(i,3) Asss(i,4) Asss(i,5)];%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A��������ʽͳһΪ[ֱ�� ��� ֱ�� ��� ʵѡ���]
              else
                  Asss0=Asss(i,5);                                         %Asss0��������ǣ��жϺ�����������Ƿ񳬹�1000mm2                  
                  Asss(i,5)= Asss(i,5)+200;                                %%%%%%%%%%%%%%�ѷ���㲻��ʱ��ʵ��ֽ��������200mm2,ѡ���,�ٽ����ѷ�����
                  for j=1:5                                                %Ƕ��ѭ���������������������1000mm2 
                       AA=fujinshuchu(Asss(i,5),Astong);                   %%%%%%%%%%%%%%�ٴ�ѡ��
                       Asss(i,:)=[AA(i,1) AA(i,2) AA(i,3) AA(i,4) AA(i,5)];%��ѡ��������Asss������
                       d(i,:)=[Asss(i,1) fix(1000/Asss(i,2)) Asss(i,3) fix(1000/Asss(i,4))]; 
                       w=liefeng1(M(i),cs,d,Asss(i,5),h,C,rg);                 
                      if w(i)<=0.2 
                         A(i,:)=[Asss(i,1) Asss(i,2) Asss(i,3) Asss(i,4) Asss(i,5)];
                         break
                      else
                         Asss(i,5)= Asss(i,5)+200;
                          if Asss(i,5)-Asss0>1000
                             disp('���ѷ�ѡ�����Ӹֽ�������޸ĵ�ǽ����');
                             return                                         %�������ӳ���
                          end
                      end
                  end
              end
        end        
      
elseif n==2         %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%2�㵲ǽ
            AsmaxA=As(1);                                       %A��������
            AsmaxB=As(3);                                       %B��������
            Astong=max(roumin*h(1),roumin*h(2));                %1��ͨ����=����ֽAB���㶼�ô�ֵ        
            AssA=fujinshuchu(AsmaxA,Astong);                   %���ú��������A�����[ֱ�� ��� ֱ�� ��� ʵѡ���]
            AssB=fujinshuchu(AsmaxB,Astong);      %���ú��������B�����[ֱ�� ��� ֱ�� ��� ʵѡ���]           
            Asss=[AssA(1)  AssA(2)  AssA(3)  AssA(4) AssA(5);
                  AssB(1)  AssB(2)  AssB(3)  AssB(4) AssB(5)];
                                                                %C�����Ϊ����ͨ����,�ѷ첻������
                                                                
        for i=1:2                                               %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A���B���ѡ��
              d(i,:)=[Asss(i,1) fix(1000/Asss(i,2)) Asss(i,3) fix(1000/Asss(i,4))];%i������ֱ��������
              w=liefeng1(M(i),cs,d,Asss(i,5),h,C,rg);                      %%%%%%����i���ѷ�
              if w<=0.2 
                 A(i,:)=[Asss(i,1) Asss(i,2) Asss(i,3) Asss(i,4) Asss(i,5)];%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A��������ʽͳһΪ[ֱ�� ��� ֱ�� ��� ʵѡ���]
              else
                  Asss0=Asss(i,5);                                         %Asss0��������ǣ��жϺ�����������Ƿ񳬹�1000mm2                  
                  Asss(i,5)= Asss(i,5)+200;                                %%%%%%%%%%%%%%�ѷ���㲻��ʱ��ʵ��ֽ��������200mm2,ѡ���,�ٽ����ѷ�����
                  for j=1:5                                                %Ƕ��ѭ���������������������1000mm2 
                       AA=fujinshuchu(Asss(i,5),Astong);                   %%%%%%%%%%%%%%�ٴ�ѡ��
                       Asss(i,:)=[AA(i,1) AA(i,2) AA(i,3) AA(i,4) AA(i,5)];%��ѡ��������Asss������
                       d(i,:)=[Asss(i,1) fix(1000/Asss(i,2)) Asss(i,3) fix(1000/Asss(i,4))]; 
                       w=liefeng1(M(i),cs,d,Asss(i,5),h,C,rg);                 
                      if w(i)<=0.2 
                         A(i,:)=[Asss(i,1) Asss(i,2) Asss(i,3) Asss(i,4) Asss(i,5)];
                         break
                      else
                         Asss(i,5)= Asss(i,5)+200;
                          if Asss(i,5)-Asss0>1000
                             disp('���ѷ�ѡ�����Ӹֽ�������޸ĵ�ǽ����');
                             return                                         %�������ӳ���
                          end
                      end
                  end
              end
        end                                                                
                                                                   
elseif n==3         %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%3�㵲ǽ
            AsmaxA=As(1);                                       %A��������
            AsmaxB=As(3);                                       %B��������
            AsmaxC=As(5);                                       %C��������              
            Astong1=max(roumin*h(1),roumin*h(2));               %1��ͨ����=����ֽAB���㶼�ô�ֵ
            Astong2=max(roumin*h(2),roumin*h(3));               %C��ͨ����
            Astong=[Astong1 Astong1 Astong2];
            AssA=fujinshuchu(AsmaxA,Astong(1));                 %���ú��������A�����[ֱ�� ��� ֱ�� ��� ʵѡ���]
            AssB=fujinshuchu(AsmaxB,Astong(2));                 %���ú��������B�����[ֱ�� ��� ֱ�� ��� ʵѡ���]
            AssC=fujinshuchu(AsmaxC,Astong(3));                 %���ú��������C�����[ֱ�� ��� ֱ�� ��� ʵѡ���]            
            Asss=[AssA(1)  AssA(2)  AssA(3)  AssA(4) AssA(5);
                  AssB(1)  AssB(2)  AssB(3)  AssB(4) AssB(5);
                  AssC(1)  AssC(2)  AssC(3)  AssC(4) AssC(5)]; 
                                                                %D�����Ϊ����ͨ����,�ѷ첻������
                                                                
        for i=1:3                                               %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A��B��C���ѡ��
              d(i,:)=[Asss(i,1) fix(1000/Asss(i,2)) Asss(i,3) fix(1000/Asss(i,4))];%i������ֱ��������
              w=liefeng1(M(i),cs,d,Asss(i,5),h,C,rg);                      %%%%%%����i���ѷ�
              if w<=0.2 
                 A(i,:)=[Asss(i,1) Asss(i,2) Asss(i,3) Asss(i,4) Asss(i,5)];%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A��������ʽͳһΪ[ֱ�� ��� ֱ�� ��� ʵѡ���]
              else
                  Asss0=Asss(i,5);                                         %Asss0��������ǣ��жϺ�����������Ƿ񳬹�1000mm2                  
                  Asss(i,5)= Asss(i,5)+200;                                %%%%%%%%%%%%%%�ѷ���㲻��ʱ��ʵ��ֽ��������200mm2,ѡ���,�ٽ����ѷ�����
                  for j=1:5                                                %Ƕ��ѭ���������������������1000mm2 
                       AA=fujinshuchu(Asss(i,5),Astong(i));                %%%%%%%%%%%%%%�ٴ�ѡ��
                       Asss(i,:)=[AA(i,1) AA(i,2) AA(i,3) AA(i,4) AA(i,5)];%��ѡ��������Asss������
                       d(i,:)=[Asss(i,1) fix(1000/Asss(i,2)) Asss(i,3) fix(1000/Asss(i,4))]; 
                       w=liefeng1(M(i),cs,d,Asss(i,5),h,C,rg);                 
                      if w(i)<=0.2 
                         A(i,:)=[Asss(i,1) Asss(i,2) Asss(i,3) Asss(i,4) Asss(i,5)];
                         break
                      else
                         Asss(i,5)= Asss(i,5)+200;
                          if Asss(i,5)-Asss0>1000
                             disp('���ѷ�ѡ�����Ӹֽ�������޸ĵ�ǽ����');
                             return                                         %�������ӳ���
                          end
                      end
                  end
              end
        end  
   
elseif n==4         %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%4�㵲ǽ
            AsmaxA=As(1);                                       %A��������
            AsmaxB=As(3);                                       %B��������
            AsmaxC=As(5);                                       %C��������
            AsmaxD=As(7);                                       %D��������
            Astong1=max(roumin*h(1),roumin*h(2));               %1��ͨ����=����ֽAB���㶼�ô�ֵ
            Astong2=max(roumin*h(2),roumin*h(3));               %C��ͨ����
            Astong3=max(roumin*h(3),roumin*h(4));
            Astong=[Astong1 Astong1 Astong2 Astong3];
            AssA=fujinshuchu(AsmaxA,Astong(1));                 %���ú��������A�����[ֱ�� ��� ֱ�� ��� ʵѡ���]
            AssB=fujinshuchu(AsmaxB,Astong(2));                 %���ú��������B�����[ֱ�� ��� ֱ�� ��� ʵѡ���]
            AssC=fujinshuchu(AsmaxC,Astong(3));                 %���ú��������C�����[ֱ�� ��� ֱ�� ��� ʵѡ���] 
            AssD=fujinshuchu(AsmaxD,Astong(4));                 %���ú��������D�����[ֱ�� ��� ֱ�� ��� ʵѡ���]
            Asss=[AssA(1)  AssA(2)  AssA(3)  AssA(4) AssA(5);
                  AssB(1)  AssB(2)  AssB(3)  AssB(4) AssB(5);
                  AssC(1)  AssC(2)  AssC(3)  AssC(4) AssC(5);
                  AssD(1)  AssD(2)  AssD(3)  AssD(4) AssD(5)]; 
                                                                %D�����Ϊ����ͨ����,�ѷ첻������
                                                                
        for i=1:4                                               %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A��B��C��D���ѡ��
              d(i,:)=[Asss(i,1) fix(1000/Asss(i,2)) Asss(i,3) fix(1000/Asss(i,4))];%i������ֱ��������
              w=liefeng1(M(i),cs,d,Asss(i,5),h,C,rg);                      %%%%%%����i���ѷ�
              if w<=0.2 
                 A(i,:)=[Asss(i,1) Asss(i,2) Asss(i,3) Asss(i,4) Asss(i,5)];%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%A��������ʽͳһΪ[ֱ�� ��� ֱ�� ��� ʵѡ���]
              else
                  Asss0=Asss(i,5);                                         %Asss0��������ǣ��жϺ�����������Ƿ񳬹�1000mm2                  
                  Asss(i,5)= Asss(i,5)+200;                                %%%%%%%%%%%%%%�ѷ���㲻��ʱ��ʵ��ֽ��������200mm2,ѡ���,�ٽ����ѷ�����
                  for j=1:5                                                %Ƕ��ѭ���������������������1000mm2 
                       AA=fujinshuchu(Asss(i,5),Astong(i));                %%%%%%%%%%%%%%�ٴ�ѡ��
                       Asss(i,:)=[AA(i,1) AA(i,2) AA(i,3) AA(i,4) AA(i,5)];%��ѡ��������Asss������
                       d(i,:)=[Asss(i,1) fix(1000/Asss(i,2)) Asss(i,3) fix(1000/Asss(i,4))]; 
                       w=liefeng1(M(i),cs,d,Asss(i,5),h,C,rg);                 
                      if w(i)<=0.2 
                         A(i,:)=[Asss(i,1) Asss(i,2) Asss(i,3) Asss(i,4) Asss(i,5)];
                         break
                      else
                         Asss(i,5)= Asss(i,5)+200;
                          if Asss(i,5)-Asss0>1000
                             disp('���ѷ�ѡ�����Ӹֽ�������޸ĵ�ǽ����');
                             return                                         %�������ӳ���
                          end
                      end
                  end
              end
        end                                                                

end
end

