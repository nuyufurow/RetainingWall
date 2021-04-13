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
   Qz150=(1000/Ass(5,2))*((Ass(5,1)/1000)^2*pi/4)*(Lz/1000)*7.85*Qg;                             %%%%%%%Qz为底筋间距150费用
   Qz200=(1000/Ass(6,2))*((Ass(6,1)/1000)^2*pi/4)*(Lz/1000)*7.85*Qg;                             %%%%%%%Qz为底筋间距200费用
   Qt=(1*h(1)/1000)*(H(1)/1000)*Qh;                                                           %%%%%%%Qt为混凝土费用
   QF150=Qft150+Qff150;                                                                       %%%%%%%QF150为间距150所有钢筋的费用         
   QF200=Qft200+Qff200;                                                                       %%%%%%%QF200为间距200所有钢筋的费用 
   Q=[   QF150          QF200        Qz150          Qz200          Qs         Qt     QF150+Qz150+Qs+Qt    QF200+Qz200+Qs+Qt];
   %  150顶筋费用     200顶筋费用   150底筋费用    200底筋费用    水平筋费用    砼费用      间距150总费用       间距200总费用
   
   
elseif n==2
   Lff1=roundn((H(1)-s(1))/3,1)+10;      %%%%%%%1层层高净高三分之一
   Lff2=roundn((H(2)-s(2))/3,1)+10;      %%%%%%%2层层高净高三分之一
   LffB=2*max(Lff1,Lff2)+s(1);           %%%%%%%B点附加筋总长度
   LffA=(H(1)-s(1))/3;                   %%%%%%%A点附加筋总长度
    
   Lft1150=H(1)+LffB+max(35*max(Ass(1,1),Ass(3,1)),500);                 %%%%%%%Lft为1层150通长顶筋的长度
   Lft1200=H(1)+LffB+max(35*max(Ass(4,1),Ass(6,1)),500);                 %%%%%%%Lft为1层150通长顶筋的长度   
   Lft2150=H(2)-max(Lff1,Lff2)-s(2)-max(35*max(Ass(1,1),Ass(3,1)),500)+maoguchangdu( CC,F,Ass(3,1) );   %%%%%%%Lft为2层间距150通长顶筋的长度
   Lft2200=H(2)-max(Lff1,Lff2)-s(2)-max(35*max(Ass(4,1),Ass(6,1)),500)+maoguchangdu( CC,F,Ass(6,1) );   %%%%%%%Lft为2层间距200通长顶筋的长度
  if h(1)==h(2)          %若1、2层墙厚相等，则用焊接
   
            Lz1150=H(1)+500+max(35*max(Ass(7,1),Ass(8,1)),500);                                           %%%%%%%Lz为间距150一层底筋的长度
            Lz2150=H(2)-500-max(35*max(Ass(7,1),Ass(8,1)),500)-s(2)+maoguchangdu(CC,F,Ass(8,1));          %%%%%%%Lz为间距150二层底筋的长度
            Lz1200=H(1)+500+max(35*max(Ass(9,1),Ass(10,1)),500);                                          %%%%%%%Lz为间距200一层底筋的长度
            Lz2200=H(2)-500-max(35*max(Ass(9,1),Ass(10,1)),500)-s(2)+maoguchangdu(CC,F,Ass(10,1));        %%%%%%%Lz为间距200二层底筋的长度            
            
   else              %1、2层墙厚不相等，采用分别锚固
        Lz1150=H(1)-cs+12*Ass(7,1);                                    %%%%间距150一层底筋长度
        laE2=maoguchangduE( Z(2),CC,F,Ass(8,1) );
        Lz2150=H(2)-s(2)+maoguchangdu( CC,F,Ass(8,1) )+1.2*laE2;       %%%%间距150二层底筋长度
        
        Lz1200=H(1)-cs+12*Ass(9,1);                                    %%%%间距200一层底筋长度
        laE2=maoguchangduE( Z(2),CC,F,Ass(10,1) );
        Lz2200=H(2)-s(2)+maoguchangdu( CC,F,Ass(10,1) )+1.2*laE2;      %%%%间距200二层底筋长度
  end 
   
   Qft150=((1000/Ass(1,2))*((Ass(1,1)/1000)^2*pi/4)*(Lft1150/1000)+(1000/Ass(3,2))*((Ass(3,1)/1000)^2*pi/4)*(Lft2150/1000))*7.85*Qg;%%%%%%%Qft为通长顶筋间距150费用
   Qff150=((1000/Ass(1,4))*((Ass(1,3)/1000)^2*pi/4)*(LffA/1000)+(1000/Ass(2,4))*((Ass(2,3)/1000)^2*pi/4)*(LffB/1000))*7.85*Qg;      %%%%%%%Qff为附加顶筋间距150费用
   Qft200=((1000/Ass(4,2))*((Ass(4,1)/1000)^2*pi/4)*(Lft1200/1000)+(1000/Ass(6,2))*((Ass(6,1)/1000)^2*pi/4)*(Lft2200/1000))*7.85*Qg;%%%%%%%Qft为通长顶筋间距200费用
   Qff200=((1000/Ass(4,4))*((Ass(4,3)/1000)^2*pi/4)*(LffA/1000)+(1000/Ass(5,4))*((Ass(5,3)/1000)^2*pi/4)*(LffB/1000))*7.85*Qg;      %%%%%%%Qff为附加顶筋间距200费用   
   Qz150=((1000/Ass(7,2))*((Ass(7,1)/1000)^2*pi/4)*(Lz1150/1000)+(1000/Ass(8,2))*((Ass(8,1)/1000)^2*pi/4)*(Lz2150/1000))*7.85*Qg;   %%%%%%%Qz间距150底筋费用
   Qz200=((1000/Ass(9,2))*((Ass(9,1)/1000)^2*pi/4)*(Lz1200/1000)+(1000/Ass(10,2))*((Ass(10,1)/1000)^2*pi/4)*(Lz2200/1000))*7.85*Qg; %%%%%%%Qz间距200底筋费用
   
   Qs=(1*2*Ashui(1,5)*H(1)+1*2*Ashui(2,5)*H(2))*7.85*Qg*10^(-9);                              %%%%%%%水平筋的费用
   
   Qt=((1*h(1)/1000)*(H(1)/1000)+(1*h(2)/1000)*(H(2)/1000))*Qh;                               %%%%%%%Qt为混凝土费用
   QF150=Qft150+Qff150;                                                                       %%%%%%%QF150为间距150所有钢筋的费用         
   QF200=Qft200+Qff200;                                                                       %%%%%%%QF200为间距200所有钢筋的费用
   
   
   Q=[QF150          QF200          Qz150           Qz200          Qt          Qs       QF150+Qz150+Qt+Qs     QF200+Qz200+Qt+Qs  ];
   % 150顶筋费用   200顶筋费用     150底筋费用    150底筋费用      砼费用     水平筋费用     间距150总费用           间距200总费用
       
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
   
  if h(1)==h(2) && h(2)==h(3)          %若1、2、3层墙厚相等，则用焊接
%%%%%%%%%%%%%%%%%%%间距150
    Lz1150 =H(1)  +500+max(35*max(Ass(9,1),Ass(10,1)),500);               
	Lz2d150=H(2)  -500-max(35*max(Ass(9,1),Ass(10,1)),500)-s(2);        

    Lz2u150=s(2)+500+max(35*max(Ass(10,1),Ass(11,1)),500);   
    la3=-s(3) + maoguchangdu(CC,F,Ass(11,1));
    Lz3150=H(3)-500-max(35*max(Ass(10,1),Ass(11,1)),500)+la3;
       
    Lz2150=Lz2u150+Lz2d150;
%%%%%%%%%%%%%%%%%%间距200           
    Lz1200=H(1)+500+max(35*max(Ass(12,1),Ass(13,1)),500);               
    Lz2d200=H(2)-500-max(35*max(Ass(12,1),Ass(13,1)),500);          

    Lz2u200=    500+max(35*max(Ass(13,1),Ass(14,1)),500);   
    la3=-s(3) + maoguchangdu(CC,F,Ass(14,1));
    Lz3200=H(3)-500-max(35*max(Ass(13,1),Ass(14,1)),500)+la3;
      
    Lz2200=Lz2u200+Lz2d200;           
       
  elseif h(1)==h(2) &&  h(2)~=h(3)          %h(1)=h(2)≠h(3)
%%%%%%%%%%%%%%%%%%%间距150      
	Lz1150=H(1)+500+max(35*max(Ass(9,1),Ass(10,1)),500);  
	Lz2150=H(2)-500-max(35*max(Ass(9,1),Ass(10,1)),500)-cs+12*Ass(10,1);        
	Lz3150=H(3)-s(3)+maoguchangdu(CC,F,Ass(11,1))+1.2*maoguchangduE(Z(3),CC,F,Ass(11,1));
%%%%%%%%%%%%%%%%%%%间距200
	Lz1200=H(1)+500+max(35*max(Ass(12,1),Ass(13,1)),500);  
	Lz2200=H(2)-500-max(35*max(Ass(12,1),Ass(13,1)),500)-cs+12*Ass(13,1);        
	Lz3200=H(3)-s(3)+maoguchangdu(CC,F,Ass(14,1))+1.2*maoguchangduE(Z(3),CC,F,Ass(14,1));

  
  elseif h(1)~=h(2) && h(2)==h(3)          %h(1)≠h(2)=h(3)
%%%%%%%%%%%%%%%%%%%间距150 
	Lz1150=H(1)-cs+12*Ass(9,1);
	Lz2150=H(2)+1.2*maoguchangduE(Z(2),CC,F,Ass(10,1))+500+max(35*max(Ass(10,1),Ass(11,1)),500);
	Lz3150=H(3)-s(3)+maoguchangdu(CC,F,Ass(11,1))-500-max(35*max(Ass(10,1),Ass(11,1)),500);
%%%%%%%%%%%%%%%%%%%间距200 
	Lz1200=H(1)-cs+12*Ass(12,1);
	Lz2200=H(2)+1.2*maoguchangduE(Z(2),CC,F,Ass(13,1))+500+max(35*max(Ass(13,1),Ass(14,1)),500);
	Lz3200=H(3)-s(3)+maoguchangdu(CC,F,Ass(14,1))-500-max(35*max(Ass(13,1),Ass(14,1)),500);

  elseif h(1)~=h(2) && h(2)~=h(3)      %h(1)≠h(2)≠h(3)
%%%%%%%%%%%%%%%%%%%间距150       
    Lz1150=H(1)-cs+12*Ass(9,1);
    Lz2150=H(2)-cs+12*Ass(10,1)+1.2*maoguchangduE(Z(2),CC,F,Ass(10,1));
    Lz3150=H(3)-s(3)+maoguchangdu(CC,F,Ass(11,1))+1.2*maoguchangduE(Z(3),CC,F,Ass(11,1));
%%%%%%%%%%%%%%%%%%%间距200 
    Lz1200=H(1)-cs+12*Ass(12,1);
    Lz2200=H(2)-cs+12*Ass(13,1)+1.2*maoguchangduE(Z(2),CC,F,Ass(13,1));
    Lz3200=H(3)-s(3)+maoguchangdu(CC,F,Ass(14,1))+1.2*maoguchangduE(Z(3),CC,F,Ass(14,1));
  end
   
   Qft150=((1000/Ass(1,2))*((Ass(1,1)/1000)^2*pi/4)*(Lft1150/1000)+(1000/Ass(3,2))*((Ass(3,1)/1000)^2*pi/4)*(Lft2150/1000)+(1000/Ass(4,2))*((Ass(4,1)/1000)^2*pi/4)*(Lft3150/1000))*7.85*Qg; %%%%%%%Qft为通长顶筋间距150费用
   Qff150=((1000/Ass(1,4))*((Ass(1,3)/1000)^2*pi/4)*(LffA/1000)+(1000/Ass(2,4))*((Ass(2,3)/1000)^2*pi/4)*(LffB/1000)+(1000/Ass(3,4))*((Ass(3,3)/1000)^2*pi/4)*(LffC/1000))*7.85*Qg;    %%%%%%%Qff为附加顶筋间距150费用
   Qft200=((1000/Ass(5,2))*((Ass(5,1)/1000)^2*pi/4)*(Lft1200/1000)+(1000/Ass(7,2))*((Ass(7,1)/1000)^2*pi/4)*(Lft2200/1000)+(1000/Ass(8,2))*((Ass(8,1)/1000)^2*pi/4)*(Lft3200/1000))*7.85*Qg;     %%%%%%%Qft为通长顶筋间距200费用
   Qff200=((1000/Ass(5,4))*((Ass(5,3)/1000)^2*pi/4)*(LffA/1000)+(1000/Ass(6,4))*((Ass(6,3)/1000)^2*pi/4)*(LffB/1000)+(1000/Ass(7,4))*((Ass(7,3)/1000)^2*pi/4)*(LffC/1000))*7.85*Qg;    %%%%%%%Qff为附加顶筋间距200费用   
   Qz150=((1000/Ass(9,2))*((Ass(9,1)/1000)^2*pi/4)*(Lz1150/1000)+(1000/Ass(10,2))*((Ass(10,1)/1000)^2*pi/4)*(Lz2150/1000)+(1000/Ass(11,2))*((Ass(11,1)/1000)^2*pi/4)*(Lz3150/1000))*7.85*Qg;          %%%%%%%Qz为间距150底筋费用
   Qz200=((1000/Ass(12,2))*((Ass(12,1)/1000)^2*pi/4)*(Lz1200/1000)+(1000/Ass(13,2))*((Ass(13,1)/1000)^2*pi/4)*(Lz2200/1000)+(1000/Ass(14,2))*((Ass(14,1)/1000)^2*pi/4)*(Lz3200/1000))*7.85*Qg;          %%%%%%%Qz为间距200底筋费用   
   Qs=(1*2*Ashui(1,5)*H(1)+1*2*Ashui(2,5)*H(2)+1*2*Ashui(3,5)*H(3))*7.85*Qg*10^(-9);                  %%%%%%%水平筋的费用
   
   Qt=((1*h(1)/1000)*(H(1)/1000)+(1*h(2)/1000)*(H(2)/1000)+(1*h(3)/1000)*(H(3)/1000))*Qh;     %%%%%%%Qt为混凝土费用
   QF150=Qft150+Qff150;                                                                       %%%%%%%QF150为间距150所有钢筋的费用         
   QF200=Qft200+Qff200;                                                                       %%%%%%%QF200为间距200所有钢筋的费用
  
   Q=[QF150          QF200          Qz150             Qz200          Qt          Qs       QF150+Qz150+Qt+Qs     QF200+Qz200+Qt+Qs  ];
   % 150顶筋费用   200顶筋费用    间距150底筋费用   间距200底筋费用    砼费用     水平筋费用     间距150总费用        间距200总费用
   
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
%%%%%%%%%%%%%%%%%%%间距150 
         Lz1150=H(1)+500+max(35*max(Ass(11,1),Ass(12,1)),500);
         Lz2150=H(2)-500-max(35*max(Ass(11,1),Ass(12,1)),500)+500+max(35*max(Ass(12,1),Ass(13,1)),500);   
         Lz3150=H(3)-500-max(35*max(Ass(12,1),Ass(13,1)),500)+500+max(35*max(Ass(13,1),Ass(14,1)),500);      
         Lz4150=H(4)-s(4)-500-max(35*max(Ass(13,1),Ass(14,1)),500)+maoguchangdu(CC,F,Ass(14,1));
%%%%%%%%%%%%%%%%%%%间距200
         Lz1200=H(1)+500+max(35*max(Ass(15,1),Ass(16,1)),500);
         Lz2200=H(2)-500-max(35*max(Ass(15,1),Ass(16,1)),500)+500+max(35*max(Ass(16,1),Ass(17,1)),500);   
         Lz3200=H(3)-500-max(35*max(Ass(16,1),Ass(17,1)),500)+500+max(35*max(Ass(17,1),Ass(18,1)),500);      
         Lz4200=H(4)-s(4)-500-max(35*max(Ass(17,1),Ass(18,1)),500)+maoguchangdu(CC,F,Ass(18,1));

  elseif h(1)==h(2) && h(2)==h(3) && h(3)~=h(4)     %有3相邻层厚度相等 h1=h2=h3≠h4
%%%%%%%%%%%%%%%%%%%间距150       
             Lz1150=H(1)+500+max(35*max(Ass(11,1),Ass(12,1)),500);
             Lz2150=H(2)-500-max(35*max(Ass(11,1),Ass(12,1)),500)+500+max(35*max(Ass(12,1),Ass(13,1)),500);
             Lz3150=H(3)-cs-500-max(35*max(Ass(12,1),Ass(13,1)),500)+12*Ass(13,1);
             Lz4150=H(4)-s(4)+maoguchangdu(CC,F,Ass(14,1))+1.2*maoguchangduE(Z(4),CC,F,Ass(14,1));
%%%%%%%%%%%%%%%%%%%间距200
             Lz1200=H(1)+500+max(35*max(Ass(15,1),Ass(16,1)),500);
             Lz2200=H(2)-500-max(35*max(Ass(15,1),Ass(16,1)),500)+500+max(35*max(Ass(16,1),Ass(17,1)),500);
             Lz3200=H(3)-cs-500-max(35*max(Ass(16,1),Ass(17,1)),500)+12*Ass(17,1);
             Lz4200=H(4)-s(4)+maoguchangdu(CC,F,Ass(18,1))+1.2*maoguchangduE(Z(4),CC,F,Ass(18,1));         
                  
  elseif h(1)~=h(2) && h(2)==h(3) && h(3)==h(4)   %有3相邻层厚度相等 h1≠h2=h3=h4
%%%%%%%%%%%%%%%%%%%间距150       
             Lz1150=H(1)-cs+12*Ass(11,1);
             Lz2150=H(2)+1.2*maoguchangduE(Z(2),CC,F,Ass(12,1))+500+max(35*max(Ass(12,1),Ass(13,1)),500);
             Lz3150=H(3)-500-max(35*max(Ass(12,1),Ass(13,1)),500)+500+max(35*max(Ass(13,1),Ass(14,1)),500);
             Lz4150=H(4)-s(4)+maoguchangdu(CC,F,Ass(14,1))-500-max(35*max(Ass(13,1),Ass(14,1)),500);
%%%%%%%%%%%%%%%%%%%间距200      
             Lz1200=H(1)-cs+12*Ass(15,1);
             Lz2200=H(2)+1.2*maoguchangduE(Z(2),CC,F,Ass(16,1))+500+max(35*max(Ass(16,1),Ass(17,1)),500);
             Lz3200=H(3)-500-max(35*max(Ass(16,1),Ass(17,1)),500)+500+max(35*max(Ass(17,1),Ass(18,1)),500);
             Lz4200=H(4)-s(4)+maoguchangdu(CC,F,Ass(18,1))-500-max(35*max(Ass(17,1),Ass(18,1)),500);      
             
  elseif h(1)==h(2) && h(2)~=h(3) && h(3)~=h(4)      %有2相邻层厚度相等 h1=h2≠h3≠h4
%%%%%%%%%%%%%%%%%%%间距150
             Lz1150=H(1)+500+max(35*max(Ass(11,1),Ass(12,1)),500);
             Lz2150=H(2)-cs-500-max(35*max(Ass(11,1),Ass(12,1)),500)+12*Ass(12,1);
             Lz3150=H(3)-cs+12*Ass(13,1)+1.2*maoguchangduE(Z(3),CC,F,Ass(13,1));
             Lz4150=H(4)-s(4)+maoguchangdu(CC,F,Ass(14,1))+1.2*maoguchangduE(Z(4),CC,F,Ass(14,1));
%%%%%%%%%%%%%%%%%%%间距200
             Lz1200=H(1)+500+max(35*max(Ass(15,1),Ass(16,1)),500);
             Lz2200=H(2)-cs-500-max(35*max(Ass(15,1),Ass(16,1)),500)+12*Ass(16,1);
             Lz3200=H(3)-cs+12*Ass(17,1)+1.2*maoguchangduE(Z(3),CC,F,Ass(17,1));
             Lz4200=H(4)-s(4)+maoguchangdu(CC,F,Ass(18,1))+1.2*maoguchangduE(Z(4),CC,F,Ass(18,1));

  elseif h(1)~=h(2) && h(2)==h(3) && h(3)~=h(4)   %有2相邻层厚度相等 h1≠h2=h3≠h4
%%%%%%%%%%%%%%%%%%%间距150      
            Lz1150=H(1)-cs+12*Ass(11,1);
            Lz2150=H(2)+1.2*maoguchangduE(Z(2),CC,F,Ass(12,1))+500+max(35*max(Ass(12,1),Ass(13,1)),500);
            Lz3150=H(3)-500-max(35*max(Ass(12,1),Ass(13,1)),500)-cs+12*Ass(13,1);      
            Lz4150=H(4)-s(4)+1.2*maoguchangduE(Z(4),CC,F,Ass(14,1))+maoguchangdu(CC,F,Ass(14,1)); 
%%%%%%%%%%%%%%%%%%%间距200
            Lz1200=H(1)-cs+12*Ass(15,1);
            Lz2200=H(2)+1.2*maoguchangduE(Z(2),CC,F,Ass(16,1))+500+max(35*max(Ass(16,1),Ass(17,1)),500);
            Lz3200=H(3)-500-max(35*max(Ass(16,1),Ass(17,1)),500)-cs+12*Ass(17,1);      
            Lz4200=H(4)-s(4)+1.2*maoguchangduE(Z(4),CC,F,Ass(18,1))+maoguchangdu(CC,F,Ass(18,1)); 

  elseif h(1)~=h(2) && h(2)~=h(3) && h(3)==h(4) %有2相邻层厚度相等 h1≠h2≠h3=h4
%%%%%%%%%%%%%%%%%%%间距150      
            Lz1150=H(1)-cs+12*Ass(11,1);
            Lz2150=H(2)-cs+12*Ass(12,1)+1.2*maoguchangduE(Z(2),CC,F,Ass(12,1));
            Lz3150=H(3)+1.2*maoguchangduE(Z(3),CC,F,Ass(13,1))+500+max(35*max(Ass(13,1),Ass(14,1)),500);
            Lz4150=H(4)-s(4)+maoguchangdu(CC,F,Ass(14,1))-500-max(35*max(Ass(13,1),Ass(14,1)),500);
%%%%%%%%%%%%%%%%%%%间距200
            Lz1200=H(1)-cs+12*Ass(15,1);
            Lz2200=H(2)-cs+12*Ass(16,1)+1.2*maoguchangduE(Z(2),CC,F,Ass(16,1));
            Lz3200=H(3)+1.2*maoguchangduE(Z(3),CC,F,Ass(17,1))+500+max(35*max(Ass(17,1),Ass(18,1)),500);
            Lz4200=H(4)-s(4)+maoguchangdu(CC,F,Ass(18,1))-500-max(35*max(Ass(17,1),Ass(18,1)),500);         
        
 elseif h(1)==h(2) && h(2)~=h(3) && h(3)==h(4)      %有2相邻层厚度相等 h1=h2≠h3=h4
%%%%%%%%%%%%%%%%%%%间距150
            Lz1150=H(1)+500+max(35*max(Ass(11,1),Ass(12,1)),500);
            Lz2150=H(2)-cs-500-max(35*max(Ass(11,1),Ass(12,1)),500)+12*Ass(12,1);
            Lz3150=H(3)+1.2*maoguchangduE(Z(3),CC,F,Ass(13,1))+500+max(35*max(Ass(13,1),Ass(14,1)),500);
            Lz4150=H(4)-s(4)+maoguchangdu(CC,F,Ass(14,1))-500-max(35*max(Ass(13,1),Ass(14,1)),500);
%%%%%%%%%%%%%%%%%%%间距200
            Lz1200=H(1)+500+max(35*max(Ass(15,1),Ass(16,1)),500);
            Lz2200=H(2)-cs-500-max(35*max(Ass(15,1),Ass(16,1)),500)+12*Ass(16,1);
            Lz3200=H(3)+1.2*maoguchangduE(Z(3),CC,F,Ass(17,1))+500+max(35*max(Ass(17,1),Ass(18,1)),500);
            Lz4200=H(4)-s(4)+maoguchangdu(CC,F,Ass(18,1))-500-max(35*max(Ass(17,1),Ass(18,1)),500);
              
  elseif  h(1)~=h(2) && h(2)~=h(3) && h(3)~=h(4)
%%%%%%%%%%%%%%%%%%%间距150
        Lz1150=H(1)-cs+12*Ass(11,1);
        Lz2150=H(2)-cs+12*Ass(12,1)+1.2*maoguchangduE(Z(2),CC,F,Ass(12,1));
        Lz3150=H(3)-cs+12*Ass(13,1)+1.2*maoguchangduE(Z(3),CC,F,Ass(13,1));
        Lz4150=H(4)-s(4)+maoguchangdu(CC,F,Ass(14,1))+1.2*maoguchangduE(Z(4),CC,F,Ass(14,1));
%%%%%%%%%%%%%%%%%%%间距200
        Lz1200=H(1)-cs+12*Ass(15,1);
        Lz2200=H(2)-cs+12*Ass(16,1)+1.2*maoguchangduE(Z(2),CC,F,Ass(16,1));
        Lz3200=H(3)-cs+12*Ass(17,1)+1.2*maoguchangduE(Z(3),CC,F,Ass(17,1));
        Lz4200=H(4)-s(4)+maoguchangdu(CC,F,Ass(18,1))+1.2*maoguchangduE(Z(4),CC,F,Ass(18,1));
  end
  
   Qft150=((1000/Ass(1,2))*((Ass(1,1)/1000)^2*pi/4)*(Lft1150/1000)+(1000/Ass(3,2))*((Ass(3,1)/1000)^2*pi/4)*(Lft2150/1000)+(1000/Ass(4,2))*((Ass(4,1)/1000)^2*pi/4)*(Lft3150/1000)+(1000/Ass(5,2))*((Ass(5,1)/1000)^2*pi/4)*(Lft4150/1000))*7.85*Qg; %%%%%%%Qft为通长顶筋间距150费用
   Qff150=((1000/Ass(1,4))*((Ass(1,3)/1000)^2*pi/4)*(LffA/1000)+(1000/Ass(2,4))*((Ass(2,3)/1000)^2*pi/4)*(LffB/1000)+(1000/Ass(3,4))*((Ass(3,3)/1000)^2*pi/4)*(LffC/1000)+(1000/Ass(4,4))*((Ass(4,3)/1000)^2*pi/4)*(LffD/1000))*7.85*Qg;%%%%%%%Qff为附加顶筋间距150费用
   Qft200=((1000/Ass(6,2))*((Ass(6,1)/1000)^2*pi/4)*(Lft1200/1000)+(1000/Ass(8,2))*((Ass(8,1)/1000)^2*pi/4)*(Lft2200/1000)+(1000/Ass(9,2))*((Ass(9,1)/1000)^2*pi/4)*(Lft3200/1000)+(1000/Ass(10,2))*((Ass(10,1)/1000)^2*pi/4)*(Lft4200/1000))*7.85*Qg;%%%%%%%Qft为通长顶筋间距200费用
   Qff200=((1000/Ass(6,4))*((Ass(6,3)/1000)^2*pi/4)*(LffA/1000)+(1000/Ass(7,4))*((Ass(7,3)/1000)^2*pi/4)*(LffB/1000)+(1000/Ass(8,4))*((Ass(8,3)/1000)^2*pi/4)*(LffC/1000)+(1000/Ass(9,4))*((Ass(9,3)/1000)^2*pi/4)*(LffD/1000))*7.85*Qg;    %%%%%%%Qff为附加顶筋间距200费用   
   Qz150=((1000/Ass(11,2))*((Ass(11,1)/1000)^2*pi/4)*(Lz1150/1000)+(1000/Ass(12,2))*((Ass(12,1)/1000)^2*pi/4)*(Lz2150/1000)+(1000/Ass(13,2))*((Ass(13,1)/1000)^2*pi/4)*(Lz3150/1000)+(1000/Ass(14,2))*((Ass(14,1)/1000)^2*pi/4)*(Lz4150/1000))*7.85*Qg; %%%%%%%Qz为间距150底筋费用
   Qz200=((1000/Ass(15,2))*((Ass(15,1)/1000)^2*pi/4)*(Lz1200/1000)+(1000/Ass(16,2))*((Ass(16,1)/1000)^2*pi/4)*(Lz2200/1000)+(1000/Ass(17,2))*((Ass(17,1)/1000)^2*pi/4)*(Lz3200/1000)+(1000/Ass(18,2))*((Ass(18,1)/1000)^2*pi/4)*(Lz4200/1000))*7.85*Qg; %%%%%%%Qz为间距200底筋费用 
   Qs=(1*2*Ashui(1,5)*H(1)+1*2*Ashui(2,5)*H(2)+1*2*Ashui(3,5)*H(3)+1*2*Ashui(4,5)*H(4))*7.85*Qg*10^(-9);                    %%%%%%%水平筋的费用
   
   Qt=((1*h(1)/1000)*(H(1)/1000)+(1*h(2)/1000)*(H(2)/1000)+(1*h(3)/1000)*(H(3)/1000)+(1*h(4)/1000)*(H(4)/1000))*Qh; %%%%%%%Qt为混凝土费用
   QF150=Qft150+Qff150;                                                                       %%%%%%%QF150为间距150所有钢筋的费用         
   QF200=Qft200+Qff200;                                                                       %%%%%%%QF200为间距200所有钢筋的费用
  
   Q=[QF150          QF200          Qz150        Qz200          Qt         Qs       QF150+Qz150+Qt+Qs     QF200+Qz200+Qt+Qs  ];
   % 150顶筋费用   200顶筋费用     150底筋费用   200底筋费用    砼费用    水平筋费用     间距150总费用     间距200总费用
  
end   
end
