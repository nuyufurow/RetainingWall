function [ Q ] = chengben( Ass,Ashui,n,Qh,Qg,h,H,cs,s,Z,CC,F)
%本子函数用于成本计算,计算结果均为每延米的价格
%Ass为zuhezhengfujin( n,As,ft,fy,h,M,cs,C,rg )计算得到的配筋结果
%Ashui——为单侧水平筋
%Qh——为混凝土单价，元/立方米
%Qg——为钢筋单价，元/吨
%n——为层数
%h——各层挡墙厚度
%H——各层挡墙高度
%cs——保护层厚度
%s——为顶板和基础之间各层板厚,从下往上数
%Z——为地下室各层抗震等级,每层抗震等级不同，Z（1）为从下往上数第一层的抗震等级
%CC——为混凝土等级 20 25 30 35 40 45 50
%F——为钢筋等级 300 335 400 500
%钢筋密度取 7.85 t/m3
%由于墙身竖向分布筋在基础内部的锚固长度与抗震等级和基础几何尺寸有关，且墙顶锚入楼板的长度（与楼板厚度有关）会影响钢筋的总长度，所以在成本计算时，不考虑钢筋锚固、弯钩等的影响。
%混凝土用方量不扣除钢筋的体积
if n==1
   la150=maoguchangdu( CC,F,Ass(1,1) );      %间距150结果的la
   la200=maoguchangdu( CC,F,Ass(2,1) );      %间距200结果的la
   laz=maoguchangdu( CC,F,Ass(2,1) );        %一层底筋长度，忽略基础中的长度
   Lft150=H(1)-s(1)+la150;                   %%%%%%%Lft为间距150通长顶筋的长度
   Lft200=H(1)-s(1)+la200;                   %%%%%%%Lft为间距200通长顶筋的长度
   Lff=roundn((H(1)-s(1))/3,1)+10;           %%%%%%%Lff为附加顶筋的长度，对十位四舍五入取整
   Lz=H(1)-s(1)+laz;                         %%%%%%%Lz为底筋的长度
   
   Qs=1*2*Ashui(1,5)*H(1)*7.85*Qg*10^(-9);                   %%%%%%%水平筋费用
   
   Qft150=(1000/Ass(1,2))*((Ass(1,1)/1000)^2*pi/4)*(Lft150/1000)*7.85*Qg;                     %%%%%%%Qft为通长顶筋间距150费用
   Qff150=(1000/Ass(1,4))*((Ass(1,3)/1000)^2*pi/4)*(Lff/1000)*7.85*Qg;                        %%%%%%%Qff为附加顶筋间距150费用
   Qft200=(1000/Ass(3,2))*((Ass(3,1)/1000)^2*pi/4)*(Lft200/1000)*7.85*Qg;                     %%%%%%%Qft为通长顶筋间距200费用
   Qff200=(1000/Ass(3,4))*((Ass(3,3)/1000)^2*pi/4)*(Lff/1000)*7.85*Qg;                        %%%%%%%Qff为附加顶筋间距200费用   
   Qz=(1000/Ass(5,2))*((Ass(5,1)/1000)^2*pi/4)*(Lz/1000)*7.85*Qg;                             %%%%%%%Qz为底筋费用
   Qt=(1*h(1)/1000)*(H(1)/1000)*Qh;                                                           %%%%%%%Qt为混凝土费用
   QF150=Qft150+Qff150;                                                                       %%%%%%%QF150为间距150所有钢筋的费用         
   QF200=Qft200+Qff200;                                                                       %%%%%%%QF200为间距200所有钢筋的费用 
   Q=[   QF150          QF200          Qz          Qs         Qt         QF150+Qz+Qs+Qt       QF200+Qz+Qs+Qt];
   %  150顶筋费用     200顶筋费用     底筋费用    水平筋费用   砼费用      顶筋间距150总费用      顶筋间距200总费用
   
   
elseif n==2
   Lff1=roundn((H(1)-s(1))/3,1)+10;      %%%%%%%1层层高净高三分之一
   Lff2=roundn((H(2)-s(2))/3,1)+10;      %%%%%%%2层层高净高三分之一
   LffB=2*max(Lff1,Lff2)+s(1);           %%%%%%%B点附加筋总长度
   LffA=(H(1)-s(1))/3;                   %%%%%%%A点附加筋总长度
    
   Lft1150=H(1)+LffB+max(35*max(Ass(1,1),Ass(3,1)),500);                 %%%%%%%Lft为1层150通长顶筋的长度
   Lft1200=H(1)+LffB+max(35*max(Ass(4,1),Ass(6,1)),500);                 %%%%%%%Lft为1层150通长顶筋的长度   
   Lft2150=H(2)-max(Lff1,Lff2)-s(2)-max(35*max(Ass(1,1),Ass(3,1)),500)+maoguchangdu( CC,F,Ass(3,1) );                              %%%%%%%Lft为2层间距150通长顶筋的长度
   Lft2200=H(2)-max(Lff1,Lff2)-s(2)-max(35*max(Ass(4,1),Ass(6,1)),500)+maoguchangdu( CC,F,Ass(6,1) );                              %%%%%%%Lft为2层间距200通长顶筋的长度
  if h(1)==h(2)          %若1、2层墙厚相等，则用焊接或分别锚固
   
        if Ass(7,2)==Ass(8,2)                  %%%一层和二层的底筋模数若相等，则采用电焊
            Lz1=H(1)+500+max(35*max(Ass(7,1),Ass(8,1)),500);                                           %%%%%%%Lz为1层底筋的长度
            Lz2=H(2)-500-max(35*max(Ass(7,1),Ass(8,1)),500)-s(2)+maoguchangdu(CC,F,Ass(8,1));          %%%%%%%Lz为2层底筋的长度     
        else                                   %%%一层和二层的底筋模数不同，则分别锚固
            laE1=maoguchangduE( Z(1),CC,F,Ass(7,1) );
            laE2=maoguchangduE( Z(2),CC,F,Ass(8,1) ); 
            Lz1=H(1)-s(1)+1.2*laE1;            %%%一层底筋长度，忽略基础中的长度
            laz=maoguchangdu( CC,F,Ass(8,1) );
            Lz2=H(2)-s(2)+laz+1.2*laE2;        %%%二层底筋长度     
        end

   else              %1、2层墙厚不相等，采用分别锚固
        Lz1=H(1)-cs+12*Ass(7,1);
        laE2=maoguchangduE( Z(2),CC,F,Ass(8,1) );
        Lz2=H(2)-s(2)+maoguchangdu( CC,F,Ass(8,1) )+1.2*laE2; 
  end 
   
   Qft150=((1000/Ass(1,2))*((Ass(1,1)/1000)^2*pi/4)*(Lft1150/1000)+(1000/Ass(3,2))*((Ass(3,1)/1000)^2*pi/4)*(Lft2150/1000))*7.85*Qg; %%%%%%%Qft为通长顶筋间距150费用
   Qff150=((1000/Ass(1,4))*((Ass(1,3)/1000)^2*pi/4)*(LffA/1000)+(1000/Ass(2,4))*((Ass(2,3)/1000)^2*pi/4)*(LffB/1000))*7.85*Qg;    %%%%%%%Qff为附加顶筋间距150费用
   Qft200=((1000/Ass(4,2))*((Ass(4,1)/1000)^2*pi/4)*(Lft1200/1000)+(1000/Ass(6,2))*((Ass(6,1)/1000)^2*pi/4)*(Lft2200/1000))*7.85*Qg; %%%%%%%Qft为通长顶筋间距200费用
   Qff200=((1000/Ass(4,4))*((Ass(4,3)/1000)^2*pi/4)*(LffA/1000)+(1000/Ass(5,4))*((Ass(5,3)/1000)^2*pi/4)*(LffB/1000))*7.85*Qg;    %%%%%%%Qff为附加顶筋间距200费用   
   Qz=((1000/Ass(7,2))*((Ass(7,1)/1000)^2*pi/4)*(Lz1/1000)+(1000/Ass(8,2))*((Ass(8,1)/1000)^2*pi/4)*(Lz2/1000))*7.85*Qg;          %%%%%%%Qz为底筋费用
   
   Qs=(1*2*Ashui(1,5)*H(1)+1*2*Ashui(2,5)*H(2))*7.85*Qg*10^(-9);                  %%%%%%%水平筋的费用
   
   Qt=((1*h(1)/1000)*(H(1)/1000)+(1*h(2)/1000)*(H(2)/1000))*Qh;                               %%%%%%%Qt为混凝土费用
   QF150=Qft150+Qff150;                                                                       %%%%%%%QF150为间距150所有钢筋的费用         
   QF200=Qft200+Qff200;                                                                       %%%%%%%QF200为间距200所有钢筋的费用
   
   
   Q=[QF150          QF200          Qz          Qt          Qs       QF150+Qz+Qt+Qs     QF200+Qz+Qt+Qs  ];
   % 150顶筋费用   200顶筋费用     底筋费用     砼费用     水平筋费用     间距150总费用     间距200总费用
       
elseif n==3
   Lff1=roundn((H(1)-s(1))/3,1)+10;      %%%%%%%1层层高净高三分之一
   Lff2=roundn((H(2)-s(2))/3,1)+10;      %%%%%%%2层层高净高三分之一
   Lff3=roundn((H(3)-s(3))/3,1)+10;      %%%%%%%3层层高净高三分之一
   LffC=2*max(Lff2,Lff3)+s(2);           %%%%%%%C点附加筋总长度   
   LffB=2*max(Lff1,Lff2)+s(1);           %%%%%%%B点附加筋总长度
   LffA=(H(1)-s(1))/3;                   %%%%%%%A点附加筋总长度
    
   Lft1150=H(1)+LffB+max(35*max(Ass(1,1),Ass(3,1)),500);                 %%%%%%%Lft1150为1层150通长顶筋的长度
   Lft2150=H(2)-max(Lff1,Lff2)-max(35*max(Ass(1,1),Ass(3,1)),500)+max(Lff2,Lff3)+max(35*max(Ass(3,1),Ass(4,1)),500); %%%%%%%Lft2150为2层间距150通长顶筋的长度
   Lft3150=H(3)-s(3)-max(Lff2,Lff3)-max(35*max(Ass(3,1),Ass(4,1)),500)+maoguchangdu(CC,F,Ass(4,1));%%%%%%%Lft3150为3层间距150通长顶筋的长度
    
   Lft1200=H(1)+LffB+max(35*max(Ass(5,1),Ass(7,1)),500);                 
   Lft2200=H(2)-max(Lff1,Lff2)-max(35*max(Ass(5,1),Ass(7,1)),500)+max(Lff2,Lff3)+max(35*max(Ass(7,1),Ass(8,1)),500); 
   Lft3200=H(3)-s(3)-max(Lff2,Lff3)-max(35*max(Ass(7,1),Ass(8,1)),500)+maoguchangdu(CC,F,Ass(8,1)); 
   
  if h(1)==h(2) && h(2)==h(3)          %若1、2、3层墙厚相等，则用焊接或分别锚固
   
        if Ass(9,2)==Ass(10,2)                  %%%一层和二层的底筋模数若相等，则采用电焊
            Lz1=H(1)+500+max(35*max(Ass(9,1),Ass(10,1)),500);                                           %%%%%%%Lz为1层底筋的长度
            Lz2d=H(2)-500-max(35*max(Ass(9,1),Ass(10,1)),500)-s(2);          %%%%%%%为2层底筋未伸入3层的长度

        else                                   %%%一层和二层的底筋模数不同，则分别锚固
            laE1=maoguchangduE( Z(1),CC,F,Ass(9,1) );
            laE2=maoguchangduE( Z(2),CC,F,Ass(10,1) ); 
            Lz1=H(1)-s(1)+1.2*laE1;            %%%一层底筋长度，忽略基础中的长度
            Lz2d=H(2)-s(2)+1.2*laE2;        %%%为2层底筋未伸入3层的长度    
        end

        if Ass(10,2)==Ass(11,2)            %%%2层和3层的底筋模数若相等，则采用电焊
           Lz2u=s(2)+500+max(35*max(Ass(10,1),Ass(11,1)),500);   
           la3=maoguchangdu(CC,F,Ass(11,1));
           Lz3=H(3)-500-max(35*max(Ass(10,1),Ass(11,1)),500)-s(3)+la3;
        else
           Lz2u=1.2*maoguchangduE(Z(2),CC,F,Ass(10,1));
           Lz3=H(3)-s(3)+maoguchangdu(CC,F,Ass(11,1))+1.2*maoguchangduE(Z(3),CC,F,Ass(11,1));   
        end
       
        Lz2=Lz2u+Lz2d;
        
        
  elseif h(1)==h(2) &&  h(2)~=h(3)          %h(1)=h(2)≠h(3)
         Lz1=H(1)-cs+12*Ass(9,1);   
         Lz2d=H(2)-s(2)+1.2*maoguchangduE(Z(2),CC,F,Ass(10,1));
         
         if Ass(10,2)==Ass(11,2)   %二层和三层底筋间距相同，采用焊接
             Lz2u=s(2)+500+max(35*max(Ass(10,1),Ass(11,1)),500);
             Lz3=H(3)-s(3)+maoguchangdu(CC,F,Ass(11,1))-500-max(35*max(Ass(10,1),Ass(11,1)),500);
         else
             Lz2u=1.2*maoguchangduE(Z(2),CC,F,Ass(10,1));
             Lz3=H(3)-s(3)+maoguchangdu(CC,F,Ass(11,1))+1.2*maoguchangduE(Z(3),CC,F,Ass(11,1));
         end
        Lz2=Lz2d+Lz2u;
 
  
  elseif h(1)~=h(2) && h(2)==h(3)          %h(1)≠h(2)=h(3)
         if Ass(9,2)==Ass(10,2)        %1层和2层底筋间距相同，采用焊接
            Lz1=H(1)+500+max(35*max(Ass(9,1),Ass(10,1)),500); 
            Lz2=H(2)-cs-500-max(35*max(Ass(9,1),Ass(10,1)),500)+12*Ass(10,1);
         else
            Lz1=H(1)-s(1)+1.2*maoguchangduE(Z(1),CC,F,Ass(9,1));
            Lz2=H(2)-cs+1.2*maoguchangduE(Z(2),CC,F,Ass(10,1))+12*Ass(10,1);
         end
            Lz3=H(3)-s(3)+maoguchangdu(CC,F,Ass(11,1))+1.2*maoguchangduE(Z(3),CC,F,Ass(11,1));
 
  
  elseif h(1)~=h(2) && h(2)~=h(3)      %h(1)≠h(2)≠h(3)
      Lz1=H(1)-cs+12*Ass(9,1);
      Lz2=H(2)-cs+12*Ass(10,1)+1.2*maoguchangduE(Z(2),CC,F,Ass(10,1));
      Lz3=H(3)-s(3)+maoguchangdu(CC,F,Ass(11,1))+1.2*maoguchangduE(Z(3),CC,F,Ass(11,1));
      
  end
   
   Qft150=((1000/Ass(1,2))*((Ass(1,1)/1000)^2*pi/4)*(Lft1150/1000)+(1000/Ass(3,2))*((Ass(3,1)/1000)^2*pi/4)*(Lft2150/1000)+(1000/Ass(4,2))*((Ass(4,1)/1000)^2*pi/4)*(Lft3150/1000))*7.85*Qg; %%%%%%%Qft为通长顶筋间距150费用
   Qff150=((1000/Ass(1,4))*((Ass(1,3)/1000)^2*pi/4)*(LffA/1000)+(1000/Ass(2,4))*((Ass(2,3)/1000)^2*pi/4)*(LffB/1000)+(1000/Ass(3,4))*((Ass(3,3)/1000)^2*pi/4)*(LffC/1000))*7.85*Qg;    %%%%%%%Qff为附加顶筋间距150费用
   Qft200=((1000/Ass(5,2))*((Ass(5,1)/1000)^2*pi/4)*(Lft1200/1000)+(1000/Ass(7,2))*((Ass(7,1)/1000)^2*pi/4)*(Lft2200/1000)+(1000/Ass(8,2))*((Ass(8,1)/1000)^2*pi/4)*(Lft3200/1000))*7.85*Qg;     %%%%%%%Qft为通长顶筋间距200费用
   Qff200=((1000/Ass(5,4))*((Ass(5,3)/1000)^2*pi/4)*(LffA/1000)+(1000/Ass(6,4))*((Ass(6,3)/1000)^2*pi/4)*(LffB/1000)+(1000/Ass(7,4))*((Ass(7,3)/1000)^2*pi/4)*(LffC/1000))*7.85*Qg;    %%%%%%%Qff为附加顶筋间距200费用   
   Qz=((1000/Ass(9,2))*((Ass(9,1)/1000)^2*pi/4)*(Lz1/1000)+(1000/Ass(10,2))*((Ass(10,1)/1000)^2*pi/4)*(Lz2/1000)+(1000/Ass(11,2))*((Ass(11,1)/1000)^2*pi/4)*(Lz3/1000))*7.85*Qg;          %%%%%%%Qz为底筋费用
   
   Qs=(1*2*Ashui(1,5)*H(1)+1*2*Ashui(2,5)*H(2)+1*2*Ashui(3,5)*H(3))*7.85*Qg*10^(-9);                  %%%%%%%水平筋的费用
   
   Qt=((1*h(1)/1000)*(H(1)/1000)+(1*h(2)/1000)*(H(2)/1000)+(1*h(3)/1000)*(H(3)/1000))*Qh;     %%%%%%%Qt为混凝土费用
   QF150=Qft150+Qff150;                                                                       %%%%%%%QF150为间距150所有钢筋的费用         
   QF200=Qft200+Qff200;                                                                       %%%%%%%QF200为间距200所有钢筋的费用
  
   Q=[QF150          QF200          Qz          Qt          Qs       QF150+Qz+Qt+Qs     QF200+Qz+Qt+Qs  ];
   % 150顶筋费用   200顶筋费用     底筋费用     砼费用     水平筋费用     间距150总费用     间距200总费用
   
elseif n==4
       Lff1=roundn((H(1)-s(1))/3,1)+10;      %%%%%%%1层层高净高三分之一
       Lff2=roundn((H(2)-s(2))/3,1)+10;      %%%%%%%2层层高净高三分之一
       Lff3=roundn((H(3)-s(3))/3,1)+10;      %%%%%%%3层层高净高三分之一
       Lff4=roundn((H(4)-s(4))/3,1)+10;      %%%%%%%4层层高净高三分之一
       LffD=2*max(Lff3,Lff4)+s(3);           %%%%%%%D点附加筋总长度
       LffC=2*max(Lff2,Lff3)+s(2);           %%%%%%%C点附加筋总长度   
       LffB=2*max(Lff1,Lff2)+s(1);           %%%%%%%B点附加筋总长度
       LffA=(H(1)-s(1))/3;                   %%%%%%%A点附加筋总长度
    
   Lft1150=H(1)+LffB+max(35*max(Ass(1,1),Ass(3,1)),500);                 %%%%%%%Lft1150为1层150通长顶筋的长度
   Lft2150=H(2)-max(Lff1,Lff2)-max(35*max(Ass(1,1),Ass(3,1)),500)+max(Lff2,Lff3)+max(35*max(Ass(3,1),Ass(4,1)),500); %%%%%%%Lft2150为2层间距150通长顶筋的长度
   Lft3150=H(3)-max(Lff2,Lff3)-max(35*max(Ass(3,1),Ass(4,1)),500)+max(Lff3,Lff4)+max(35*max(Ass(4,1),Ass(5,1)),500); %%%%%%%Lft3150为3层间距150通长顶筋的长度
   Lft4150=H(4)-s(4)-max(Lff3,Lff4)-max(35*max(Ass(4,1),Ass(5,1)),500)+maoguchangdu(CC,F,Ass(5,1));                  %%%%%%%Lft3150为4层间距150通长顶筋的长度
   
   Lft1200=H(1)+LffB+max(35*max(Ass(6,1),Ass(8,1)),500);                 %%%%%%%Lft1200为1层200通长顶筋的长度
   Lft2200=H(2)-max(Lff1,Lff2)-max(35*max(Ass(6,1),Ass(8,1)),500)+max(Lff2,Lff3)+max(35*max(Ass(8,1),Ass(9,1)),500); 
   Lft3200=H(3)-max(Lff2,Lff3)-max(35*max(Ass(8,1),Ass(9,1)),500)+max(Lff3,Lff4)+max(35*max(Ass(9,1),Ass(10,1)),500); 
   Lft4200=H(4)-s(4)-max(Lff3,Lff4)-max(35*max(Ass(9,1),Ass(10,1)),500)+maoguchangdu(CC,F,Ass(10,1));                   
   
  if h(1)==h(2) && h(2)==h(3) && h(3)==h(4)          %若1、2、3、4层墙厚相等，则用焊接或分别锚固
      if Ass(11,2)==Ass(12,2)        %若1、2层底筋间距模数相同，则采用焊接
          Lz1=H(1)+500+max(35*max(Ass(11,1),Ass(12,1)),500);
          Lz2d=H(2)-s(2)-500-max(35*max(Ass(11,1),Ass(12,1)),500);   
      else
          Lz1=H(1)-s(1)+1.2*maoguchangduE(Z(1),CC,F,Ass(11,1));
          Lz2d=H(2)-s(2)+1.2*maoguchangduE(Z(2),CC,F,Ass(12,1));
      end
       
      if Ass(12,2)==Ass(13,2)        %若2、3层底筋间距模数相同，则采用焊接
          Lz2u=s(2)+500+max(35*max(Ass(12,1),Ass(13,1)),500);
          Lz3d=H(3)-s(3)-500-max(35*max(Ass(12,1),Ass(13,1)),500);
      else
          Lz2u=1.2*maoguchangduE(Z(2),CC,F,Ass(12,1));
          Lz3d=H(3)-s(3)+1.2*maoguchangduE(Z(3),CC,F,Ass(13,1));
      end
      
      if Ass(13,2)==Ass(14,2)        %若3、4层底筋间距模数相同，则采用焊接
         Lz3u=s(3)+500+max(max(Ass(13,1),Ass(14,1)),500);
         Lz4=H(4)-s(4)-500-max(max(Ass(13,1),Ass(14,1)),500)+maoguchangdu(CC,F,Ass(14,1));
      else
         Lz3u=1.2*maoguchangduE(Z(3),CC,F,Ass(13,1));
         Lz4=H(4)-s(4)-500-max(max(Ass(13,1),Ass(14,1)),500)+maoguchangdu(CC,F,Ass(14,1));  
      end
        
       Lz2=Lz2d+Lz2u;
       Lz3=Lz3d+Lz3u;
      
  elseif h(1)==h(2) && h(2)==h(3) && h(3)~=h(4)     %有3相邻层厚度相等 h1=h2=h3≠h4
         if Ass(11,2)==Ass(12,2)                    %若1、2层底筋间距模数相同，则采用焊接
             Lz1=H(1)+500+max(35*max(Ass(11,1),Ass(12,1)),500);
             Lz2d=H(2)-s(2)-500-max(35*max(Ass(11,1),Ass(12,1)),500);
         else
             Lz1=H(1)-s(1)+1.2*maoguchangduE(Z(1),CC,F,Ass(11,1));
             Lz2d=H(2)-s(2)+1.2*maoguchangduE(Z(2),CC,F,Ass(12,1));
         end
         
         if Ass(12,2)==Ass(13,2)                   %若2、3层底筋间距模数相同，则采用焊接
            Lz2u=s(2)+500+max(35*max(Ass(12,1),Ass(13,1)),500);
            Lz3=H(3)-cs-500-max(35*max(Ass(12,1),Ass(13,1)),500)+12*Ass(13,1);
         else
            Lz2u=1.2*maoguchangduE(Z(2),CC,F,Ass(12,1));
            Lz3=H(3)-cs+1.2*maoguchangduE(Z(3),CC,F,Ass(13,1))+12*Ass(13,1);
         end
         
         Lz2=Lz2d+Lz2u;
         Lz4=H(4)-s(4)+maoguchangdu(CC,F,Ass(14,1))+1.2*maoguchangduE(Z(4),CC,F,Ass(14,1));
         
  elseif h(1)~=h(2) && h(2)==h(3) && h(3)==h(4)   %有3相邻层厚度相等 h1≠h2=h3=h4
        Lz1=H(1)-cs+12*Ass(11,1);
        Lz2d=H(2)-s(2)+1.2*maoguchangduE(Z(1),CC,F,Ass(12,1));
        if Ass(12,2)==Ass(13,2)
           Lz2u=s(2)+500+max(35*max(Ass(12,1),Ass(13,1)),500);
           Lz3d=H(3)-s(3)-500-max(35*max(Ass(12,1),Ass(13,1)),500);
        else
           Lz2u=1.2*maoguchangduE(Z(2),CC,F,Ass(12,1));
           Lz3d=H(3)-s(3)+1.2*maoguchangduE(Z(3),CC,F,Ass(13,1));
        end
        
        if Ass(13,2)==Ass(14,2)
            Lz3u=s(3)+500+max(35*max(Ass(13,1),Ass(14,1)),500);
            Lz4=H(4)-s(4)+maoguchangdu(CC,F,Ass(14,1))-500-max(35*max(Ass(13,1),Ass(14,1)),500);
        else
            Lz3u=1.2*maoguchangduE(Z(3),CC,F,Ass(13,1));
            Lz4=H(4)-s(4)+maoguchangdu(CC,F,Ass(14,1))+1.2*maoguchangduE(Z(4),CC,F,Ass(14,1));
        end
           
        Lz2=Lz2d+Lz2u;
        Lz3=Lz3d+Lz3u;
        
  elseif h(1)==h(2) && h(2)~=h(3) && h(3)~=h(4)      %有2相邻层厚度相等 h1=h2≠h3≠h4
         if Ass(11,2)==Ass(12,2)
             Lz1=H(1)+500+max(35*max(Ass(11,1),Ass(12,1)),500);
             Lz2=H(2)-cs-500-max(35*max(Ass(11,1),Ass(12,1)),500)+12*Ass(12,1);
         else
             Lz1=H(1)-s(1)+1.2*maoguchangduE(Z(1),CC,F,Ass(11,1));
             Lz2=H(2)-cs+12*Ass(12,1)+1.2*maoguchangduE(Z(2),CC,F,Ass(12,1));
         end
         
            Lz3=H(3)-cs+12*Ass(13,1)+1.2*maoguchangduE(Z(3),CC,F,Ass(13,1));
            Lz4=H(4)-s(4)+maoguchangdu(CC,F,Ass(14,1))+1.2*maoguchangduE(Z(4),CC,F,Ass(14,1));
            
  elseif h(1)~=h(2) && h(2)==h(3) && h(3)~=h(4)   %有2相邻层厚度相等 h1≠h2=h3≠h4
         Lz1=H(1)-cs+12*Ass(11,1);
         Lz2d=H(2)-s(2)+1.2*maoguchangduE(Z(2),CC,F,Ass(12,1));
         if Ass(12,2)==Ass(13,2)
            Lz2u=s(2)+500+max(35*max(Ass(12,1),Ass(13,1)),500);
            Lz3=H(3)-500-max(35*max(Ass(12,1),Ass(13,1)),500)-cs+12*Ass(13,1);
         else
            Lz2u=1.2*maoguchangduE(Z(2),CC,F,Ass(12,1));
            Lz3=H(3)-cs+12*Ass(13,1)+1.2*maoguchangduE(Z(3),CC,F,Ass(13,1));
         end
                  
            Lz4=H(4)-s(4)+1.2*maoguchangduE(Z(4),CC,F,Ass(14,1))+maoguchangdu(CC,F,Ass(14,1)); 
            Lz2=Lz2u+Lz2d;
            
  elseif h(1)~=h(2) && h(2)~=h(3) && h(3)==h(4)
         Lz1=H(1)-cs+12*Ass(11,1);
         Lz2=H(2)-cs+12*Ass(12,1)+1.2*maoguchangduE(Z(1),CC,F,Ass(12,1));
         Lz3d=H(3)-s(3)+1.2*maoguchangduE(Z(3),CC,F,Ass(13,1));
         if Ass(13,2)==Ass(14,2)
             Lz3u=s(3)+500+max(35*max(Ass(13,1),Ass(14,1)),500);
             Lz4=H(4)-s(4)+maoguchangdu(CC,F,Ass(14,1))-500-max(35*max(Ass(13,1),Ass(14,1)),500);
         else
             Lz3u=1.2*maoguchangduE(Z(3),CC,F,Ass(13,1));
             Lz4=H(4)-s(4)+maoguchangdu(CC,F,Ass(14,1))+1.2*maoguchangduE(Z(4),CC,F,Ass(14,1));
         end
      
         Lz3=Lz3u+Lz3d;
         
 elseif h(1)==h(2) && h(2)~=h(3) && h(3)==h(4)      %有2相邻层厚度相等 h1=h2≠h3=h4
         if Ass(11,2)==Ass(12,2)
             Lz1=H(1)+500+max(35*max(Ass(11,1),Ass(12,1)),500);
             Lz2=H(2)-cs-500-max(35*max(Ass(11,1),Ass(12,1)),500)+12*Ass(12,1);
         else
             Lz1=H(1)-s(1)+1.2*maoguchangduE(Z(1),CC,F,Ass(11,1));
             Lz2=H(2)-cs+12*Ass(12,1)+1.2*maoguchangduE(Z(2),CC,F,Ass(12,1));
         end
         
         if Ass(13,2)==Ass(14,2)
            Lz3=H(3)+1.2*maoguchangduE(Z(3),CC,F,Ass(13,1))+500+max(35*max(Ass(13,1),Ass(14,1)),500);
            Lz4=H(4)-s(4)+maoguchangdu(CC,F,Ass(14,1))-500-max(35*max(Ass(13,1),Ass(14,1)),500);
         else 
            Lz3=H(3)-s(3)+1.2*maoguchangduE(Z(3),CC,F,Ass(13,1))*2; 
            Lz4=H(4)-s(4)+maoguchangdu(CC,F,Ass(14,1))+1.2*maoguchangduE(Z(4),CC,F,Ass(14,1));
         end
              
  elseif  h(1)~=h(2) && h(2)~=h(3) && h(3)~=h(4)
        Lz1=H(1)-cs+12*Ass(11,1);
        Lz2=H(2)-cs+12*Ass(12,1)+1.2*maoguchangduE(Z(2),CC,F,Ass(12,1));
        Lz3=H(3)-cs+12*Ass(13,1)+1.2*maoguchangduE(Z(3),CC,F,Ass(13,1));
        Lz4=H(4)-s(4)+maoguchangdu(CC,F,Ass(14,1))+1.2*maoguchangduE(Z(4),CC,F,Ass(14,1));
  end
  
   Qft150=((1000/Ass(1,2))*((Ass(1,1)/1000)^2*pi/4)*(Lft1150/1000)+(1000/Ass(3,2))*((Ass(3,1)/1000)^2*pi/4)*(Lft2150/1000)+(1000/Ass(4,2))*((Ass(4,1)/1000)^2*pi/4)*(Lft3150/1000)+(1000/Ass(5,2))*((Ass(5,1)/1000)^2*pi/4)*(Lft4150/1000))*7.85*Qg; %%%%%%%Qft为通长顶筋间距150费用
   Qff150=((1000/Ass(1,4))*((Ass(1,3)/1000)^2*pi/4)*(LffA/1000)+(1000/Ass(2,4))*((Ass(2,3)/1000)^2*pi/4)*(LffB/1000)+(1000/Ass(3,4))*((Ass(3,3)/1000)^2*pi/4)*(LffC/1000)+(1000/Ass(4,4))*((Ass(4,3)/1000)^2*pi/4)*(LffD/1000))*7.85*Qg;%%%%%%%Qff为附加顶筋间距150费用
   Qft200=((1000/Ass(6,2))*((Ass(6,1)/1000)^2*pi/4)*(Lft1200/1000)+(1000/Ass(8,2))*((Ass(8,1)/1000)^2*pi/4)*(Lft2200/1000)+(1000/Ass(9,2))*((Ass(9,1)/1000)^2*pi/4)*(Lft3200/1000)+(1000/Ass(10,2))*((Ass(10,1)/1000)^2*pi/4)*(Lft4200/1000))*7.85*Qg;%%%%%%%Qft为通长顶筋间距200费用
   Qff200=((1000/Ass(6,4))*((Ass(6,3)/1000)^2*pi/4)*(LffA/1000)+(1000/Ass(7,4))*((Ass(7,3)/1000)^2*pi/4)*(LffB/1000)+(1000/Ass(8,4))*((Ass(8,3)/1000)^2*pi/4)*(LffC/1000)+(1000/Ass(9,4))*((Ass(9,3)/1000)^2*pi/4)*(LffD/1000))*7.85*Qg;    %%%%%%%Qff为附加顶筋间距200费用   
   Qz=((1000/Ass(11,2))*((Ass(11,1)/1000)^2*pi/4)*(Lz1/1000)+(1000/Ass(12,2))*((Ass(12,1)/1000)^2*pi/4)*(Lz2/1000)+(1000/Ass(13,2))*((Ass(13,1)/1000)^2*pi/4)*(Lz3/1000)+(1000/Ass(14,2))*((Ass(14,1)/1000)^2*pi/4)*(Lz4/1000))*7.85*Qg;          %%%%%%%Qz为底筋费用
   
   Qs=(1*2*Ashui(1,5)*H(1)+1*2*Ashui(2,5)*H(2)+1*2*Ashui(3,5)*H(3)+1*2*Ashui(4,5)*H(4))*7.85*Qg*10^(-9);                    %%%%%%%水平筋的费用
   
   Qt=((1*h(1)/1000)*(H(1)/1000)+(1*h(2)/1000)*(H(2)/1000)+(1*h(3)/1000)*(H(3)/1000)+(1*h(4)/1000)*(H(4)/1000))*Qh; %%%%%%%Qt为混凝土费用
   QF150=Qft150+Qff150;                                                                       %%%%%%%QF150为间距150所有钢筋的费用         
   QF200=Qft200+Qff200;                                                                       %%%%%%%QF200为间距200所有钢筋的费用
  
   Q=[QF150          QF200          Qz          Qt          Qs       QF150+Qz+Qt+Qs     QF200+Qz+Qt+Qs  ];
   % 150顶筋费用   200顶筋费用     底筋费用     砼费用     水平筋费用     间距150总费用     间距200总费用
  
end   
end
