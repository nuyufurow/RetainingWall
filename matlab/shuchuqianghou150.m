function [ target ] = shuchuqianghou150( n,C,H,r,hh,p0,rg,rq,F,Qh,Qg,Z,s,cs,T )
%本子函数为收尾函数，用于输出最优墙厚（顶筋间距150），并用最优墙厚再进行内力、配筋、选筋计算输出到计算书
%AAAA为zuiyouqianghou(n,C,H,r,hh,p0,rg,rq,F,Qh,Qg,Z,s,cs,T)得到的与n相应的所有墙厚组合下的成本
AAAA=zuiyouqianghou(n,C,H,r,hh,p0,rg,rq,F,Qh,Qg,Z,s,cs,T);
BB=[4 10 20 35];%用于储存n对应的墙厚种类数
if n==1
B=BB(n) ; 
hhhh=[250; 300; 350; 400];
  for i=1:B
      if min([AAAA(i,1) AAAA(i,3) AAAA(i,5)])==0    %把AAAA中的有0的行找出来，并赋大值与该行方便后续找最经济墙厚
         AAAA(i,:)=[999999 999999 999999 999999 999999 999999];
      end
  end
  ss=AAAA(:,5);
  sss=find(ss==min(ss));%%%%%返回最优成本行号
  if max([AAAA(sss,1) AAAA(sss,3) AAAA(sss,5)])==999999 %若所有解配筋均存在0值，则表明内力超出程序上限或裂缝选筋超出程序上限，无法配筋计算和选筋
      disp('内力超出程序上限或选筋超出程序上限，计算失败'); 
      return
  end
  h=hhhh(sss,:);    %得到最优墙厚  
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%得到最优墙厚后，在运行一次计算程序，用于填写计算书
hunningtu=tongdengji(C);%混凝土标号
E=hunningtu(2);%砼弹性模量
fc=hunningtu(1);%砼抗压强度设计值
A=1000*h;%截面积
I=1000*h.^3/12;%截面惯性矩
fy=gangjindengji(F);%钢筋抗拉强度设计值
ft=hunningtu(4);%混凝土抗拉强度设计值
K=zonggangjuzhen(n,E,A,I,H);%得到总刚度矩阵
Q=hezaijisuan(n,H,r,hh,p0,rg,rq);%得到计算荷载
f01=dengxiaojiedianhezai01(n,H,Q);%得到墙底固结时等效节点荷载，已划行划列
f02=dengxiaojiedianhezai02(n,H,Q);%得到墙底铰结时等效节点荷载，已划行划列
K01=weiyibianjie01(n,K);%得到划行划列之后，墙底固结的总刚度矩阵
K02=weiyibianjie02(n,K);%得到划行划列之后，墙底铰结的总刚度矩阵
M01=neilijisuan01(E,A,I,K01,f01,n,Q,H);%得到墙底固结时，各节点内力
M02=neilijisuan02(E,A,I,K02,f02,n,Q,H);%得到墙底铰结时，各节点内力
MM=neilitiaofu(M01,M02,T);%进行内力调幅，得到配筋计算所需要个节点弯矩和剪力
Mmax=kuazhongzuidaM(MM,n,Q,H);%得到内力调幅后跨中最大弯矩
M=neilizuhe(n,MM,Mmax);%将负弯矩和正弯矩集成到一个列向量中，方便后续调用
As=peijinjisuan(M,cs,n,h,fy,fc,ft);%得到各节点和跨中的计算配筋值，满足最小配筋率
Ass=zuhezhengfujin(n,As,ft,fy,h,M,cs,C,rg);%得到间距150顶筋、间距200顶筋和底筋
Ashui=shuipingjin(n,h);%得到水平筋
Money=chengben(Ass,Ashui,n,Qh,Qg,h,H,cs,s,Z,C,F); %计算成本 

d=[Ass(1,1) fix(1000/Ass(1,2)) Ass(1,3) fix(1000/Ass(1,4))];  %重新根据实配筋计算A点裂缝，用于填计算书
wA=liefeng2(M(1),cs,d,Ass(1,5),h,C,rg); 
d=[Ass(5,1) fix(1000/Ass(5,2)) 0 0];                          %重新根据实配筋计算AB裂缝，用于填计算书
wAB=liefeng2(M(2),cs,d,Ass(5,5),h,C,rg);

%%%%%%%%%%%%%%%%%%%%%%%%%%22x6的矩阵，用于填写计算书，格式同计算书
target=[E 0 0 0 0 0;                                              %砼弹性模量，MPa
        fc 0 0 0 0 0;                                             %砼抗压强度，MPa
        hunningtu(3) 0 0 0 0 0;                                   %砼抗拉强度标准值，MPa
        fy 0 0 0 0 0;                                             %钢筋强度，MPa
        2.0*10^5 0 0 0 0 0;                                       %钢筋弹模，MPa
        h 0 0 0 0 0;                                              %最优墙厚
        max(0.2/100,45*ft/fy) 0 0 0 0 0;                          %最小配筋率
        Q(1) Q(2) 0 0 0 0;                                        %A点和B点的荷载设计值 ,kN/m
        M01(1,1)/10^6 M01(2,1)/10^3 M01(2,2)/10^3 0 0 0;          %调幅前A点弯矩设计值kN/m、A点支反力kN、B点支反力kN
        MM(1,1)/10^6 MM(2,1)/10^3 MM(2,2)/10^3 0 0 0;             %调幅后A点弯矩设计值kN/m、A点支反力kN、B点支反力kN
        M(2)/10^6 0 0 0 0 0;                                      %AB跨中最大弯矩，kN·m
        h-cs-16/2 0 0 0 0 0;                                      %配筋计算假定直径为16mm时的h0，mm
        0.8/(1+fy/(2.0*10^5*0.0033)) 0 0 0 0 0;                   %界限相对受压区高度
        As(3,1) As(2,1) As(4,1) 0 0 0;                            %A点的受压区高度xA(mm)、计算纵筋（mm2）、相对受压区高度
        As(3,2) As(2,2) As(4,2) 0 0 0;                            %AB跨中的受压区高度xA(mm)、计算纵筋（mm2）、相对受压区高度 
        As(2,3) Ass(2,1) Ass(2,2) 0        0        Ass(2,5);     %B点计算面积(mm2)、实配直径(mm)、实配间距（mm）、实配面积（mm2）
        As(2,1) Ass(1,1) Ass(1,2) Ass(1,3) Ass(1,4) Ass(1,5);     %A点计算面积(mm2)、实配通长直径(mm)、实配通长间距（mm）、实配附加直径(mm)、实配附加间距（mm）、实配面积（mm2)
        As(2,2) Ass(5,1) Ass(5,2) 0        0        Ass(5,5);     %AB跨中计算面积(mm2)、实配直径(mm)、实配间距（mm）、实配面积（mm2)
        Ashui(1,1) Ashui(1,2) 0 0 Ashui(1,5) 0;                   %一层挡墙的水平筋单侧直径（mm）、间距（mm）、面积（mm2）
        M(1)/10^6/rg M(3)/10^6/rg 0 0 0 0;                        %A点准永久弯矩、B点准永久弯矩、AB跨中准永久弯矩，kN·m
        wA(2) wA(3) wA(4) wA(5) wA(1) 0;                          %A点seigema（MPa）、route、fai、de（mm）、裂缝宽度（mm）
        wAB(2) wAB(3) wAB(4) wAB(5) wAB(1) 0;                     %AB跨中seigema（MPa）、route、fai、de（mm）、裂缝宽度（mm）
        Money(1) Money(3) Money(5) Money(6) Money(7) 0];          %顶筋间距150时，顶筋费用、底筋费、砼费用、水平筋费用、总费用
    
elseif n==2    
B=BB(n) ; 
hhhh=[250 250;300 300;350 350;400 400;
      400 250;400 300;400 350;350 250;
      350 300;300 250];
  for i=1:B
      if min([AAAA(i,1) AAAA(i,3) AAAA(i,5)])==0    %把AAAA中的有0的行找出来，并赋大值与该行方便后续找最经济墙厚
         AAAA(i,:)=[999999 999999 999999 999999 999999 999999];
      end
  end
  ss=AAAA(:,5);
  sss=find(ss==min(ss));%%%%%返回最优成本行号
  if max([AAAA(sss,1) AAAA(sss,3) AAAA(sss,5)])==999999 %若所有解配筋均存在0值，则表明内力超出程序上限或裂缝选筋超出程序上限，无法配筋计算和选筋
      disp('内力超出程序上限或选筋超出程序上限，计算失败'); 
      return
  end
  h=hhhh(sss,:);    %得到最优墙厚    
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%得到最优墙厚后，在运行一次计算程序，用于填写计算书
hunningtu=tongdengji(C);%混凝土标号
E=hunningtu(2);%砼弹性模量
fc=hunningtu(1);%砼抗压强度设计值
A=1000*h;%截面积
I=1000*h.^3/12;%截面惯性矩
fy=gangjindengji(F);%钢筋抗拉强度设计值
ft=hunningtu(4);%混凝土抗拉强度设计值
K=zonggangjuzhen(n,E,A,I,H);%得到总刚度矩阵
Q=hezaijisuan(n,H,r,hh,p0,rg,rq);%得到计算荷载
f01=dengxiaojiedianhezai01(n,H,Q);%得到墙底固结时等效节点荷载，已划行划列
f02=dengxiaojiedianhezai02(n,H,Q);%得到墙底铰结时等效节点荷载，已划行划列
K01=weiyibianjie01(n,K);%得到划行划列之后，墙底固结的总刚度矩阵
K02=weiyibianjie02(n,K);%得到划行划列之后，墙底铰结的总刚度矩阵
M01=neilijisuan01(E,A,I,K01,f01,n,Q,H);%得到墙底固结时，各节点内力
M02=neilijisuan02(E,A,I,K02,f02,n,Q,H);%得到墙底铰结时，各节点内力
MM=neilitiaofu(M01,M02,T);%进行内力调幅，得到配筋计算所需要个节点弯矩和剪力
Mmax=kuazhongzuidaM(MM,n,Q,H);%得到内力调幅后跨中最大弯矩
M=neilizuhe(n,MM,Mmax);%将负弯矩和正弯矩集成到一个列向量中，方便后续调用
As=peijinjisuan(M,cs,n,h,fy,fc,ft);%得到各节点和跨中的计算配筋值，满足最小配筋率
Ass=zuhezhengfujin(n,As,ft,fy,h,M,cs,C,rg);%得到间距150顶筋、间距200顶筋和底筋
Ashui=shuipingjin(n,h);%得到水平筋
Money=chengben(Ass,Ashui,n,Qh,Qg,h,H,cs,s,Z,C,F); %计算成本     

d=[Ass(1,1) fix(1000/Ass(1,2)) Ass(1,3) fix(1000/Ass(1,4))];  %重新根据实配筋计算A点裂缝，用于填计算书
wA=liefeng2(M(1),cs,d,Ass(1,5),h(1),C,rg); 
d=[Ass(7,1) fix(1000/Ass(7,2)) 0 0];                          %重新根据实配筋计算AB裂缝，用于填计算书
wAB=liefeng2(M(2),cs,d,Ass(7,5),h(1),C,rg);  
d=[Ass(2,1) fix(1000/Ass(2,2)) Ass(2,3) fix(1000/Ass(2,4))];  %重新根据实配筋计算B点左侧裂缝，用于填计算书
wBl=liefeng2(M(3),cs,d,Ass(2,5),h(1),C,rg); 
d=[Ass(2,1) fix(1000/Ass(2,2)) Ass(2,3) fix(1000/Ass(2,4))];  %重新根据实配筋计算B点右侧裂缝，用于填计算书
wBr=liefeng2(M(3),cs,d,Ass(2,5),h(2),C,rg); 
d=[Ass(8,1) fix(1000/Ass(8,2)) 0 0];                          %重新根据实配筋计算BC裂缝，用于填计算书
wBC=liefeng2(M(4),cs,d,Ass(8,5),h(2),C,rg);

%%%%%%%%%%%%%%%%%%%%%%%%%% 37x6 的矩阵，用于填写计算书，格式同计算书
target=[E 0 0 0 0 0;                                              %砼弹性模量，MPa
        fc 0 0 0 0 0;                                             %砼抗压强度，MPa
        hunningtu(3) 0 0 0 0 0;                                   %砼抗拉强度标准值，MPa
        fy 0 0 0 0 0;                                             %钢筋强度，MPa
        2.0*10^5 0 0 0 0 0;                                       %钢筋弹模，MPa
        h(1) h(2) 0 0 0 0;                                        %最优墙厚
        max(0.2/100,45*ft/fy) 0 0 0 0 0;                          %最小配筋率
        Q(1) Q(2) Q(3) 0 0 0;                                     %A、B、C点的荷载设计值 ,kN/m
        M01(1,1)/10^6 M01(1,2)/10^6 M01(1,3)/10^6 0 0 0;          %墙底固结A点弯矩设计值、B点左侧弯矩设计值、B点右侧弯矩设计值，kN/m
        M01(2,1)/10^3 M01(2,2)/10^3 M01(2,3)/10^3 0 0 0;          %墙底固结A点支反力、B点支反力，C点支反力，kN
        0             M02(1,2)/10^6 M02(1,3)/10^6 0 0 0;          %墙底铰结A点弯矩设计值、B点左侧弯矩设计值、B点右侧弯矩设计值，kN/m
        M02(2,1)/10^3 M02(2,2)/10^3 M02(2,3)/10^3 0 0 0;          %墙底铰结A点支反力、B点支反力，C点支反力，kN        
        MM(1,1)/10^6 MM(1,2)/10^6 MM(1,3)/10^6 0 0 0;             %调幅后A点弯矩设计值、B点左侧弯矩设计值、B点右侧弯矩设计值，kN/m
        MM(2,1)/10^3 MM(2,2)/10^3 MM(2,3)/10^3 0 0 0;             %调幅后A点支反力、B点支反力，C点支反力，kN         
        M(2)/10^6 M(4)/10^6 0 0 0 0;                              %AB、BC跨中最大弯矩，kN·m
        h(1)-cs-16/2 h(2)-cs-16/2 0 0 0 0;                        %配筋计算假定直径为16mm时的h0，mm
        0.8/(1+fy/(2.0*10^5*0.0033)) 0 0 0 0 0;                   %界限相对受压区高度
        As(3,5) As(2,5) As(4,5) 0 0 0;                            %BC跨中的受压区高度x(mm)、计算纵筋（mm2）、相对受压区高度
        As(3,4) As(2,4) As(4,4) 0 0 0;                            %B点右侧受压区高度x(mm)、计算纵筋（mm2）、相对受压区高度
        As(3,3) As(2,3) As(4,3) 0 0 0;                            %B点左侧受压区高度x(mm)、计算纵筋（mm2）、相对受压区高度
        As(3,2) As(2,2) As(4,2) 0 0 0;                            %AB跨中的受压区高度x(mm)、计算纵筋（mm2）、相对受压区高度
        As(3,1) As(2,1) As(4,1) 0 0 0;                            %A点的受压区高度x(mm)、计算纵筋（mm2）、相对受压区高度  
        As(1,1) Ass(1,1) Ass(1,2) Ass(1,3) Ass(1,4) Ass(1,5);     %A点计算面积(mm2)、实配通长直径(mm)、实配通长间距（mm）、实配附加直径(mm)、实配附加间距（mm）、实配面积（mm2)
        As(1,3) Ass(2,1) Ass(2,2) Ass(2,3) Ass(2,4) Ass(2,5);     %B点计算面积(mm2)、实配通长直径(mm)、实配通长间距（mm）、实配附加直径(mm)、实配附加间距（mm）、实配面积（mm2)
        As(1,5) Ass(3,1) Ass(3,2) 0        0        Ass(3,5);     %C点计算面积(mm2)、实配通长直径(mm)、实配通长间距（mm）、实配面积（mm2)
        As(1,2) Ass(7,1) Ass(7,2) 0        0        Ass(7,5);     %AB跨中计算面积(mm2)、实配直径(mm)、实配间距（mm）、实配面积（mm2)
        As(1,4) Ass(8,1) Ass(8,2) 0        0        Ass(8,5);     %BC跨中计算面积(mm2)、实配直径(mm)、实配间距（mm）、实配面积（mm2)
        Ashui(1,1) Ashui(1,2) 0 0 Ashui(1,5) 0;                   %一层挡墙的水平筋单侧直径（mm）、间距（mm）、面积（mm2）
        Ashui(2,1) Ashui(2,2) 0 0 Ashui(2,5) 0;                   %二层挡墙的水平筋单侧直径（mm）、间距（mm）、面积（mm2）
        MM(1)/10^6/rg MM(2)/10^6/rg MM(3)/10^6/rg 0 0 0;          %A点准永久弯矩、B点左侧准永久弯矩、B点右侧准永久弯矩，kN·m
        M(2)/10^6/rg M(4)/10^6/rg 0 0 0 0 ;                       %AB准永久弯矩、BC准永久弯矩，kN·m  
        wA(2) wA(3) wA(4) wA(5) wA(1) 0;                          %A点seigema（MPa）、route、fai、de（mm）、裂缝宽度（mm）
        wAB(2) wAB(3) wAB(4) wAB(5) wAB(1) 0;                     %AB跨中seigema（MPa）、route、fai、de（mm）、裂缝宽度（mm）
        wBl(2) wBl(3) wBl(4) wBl(5) wBl(1) 0;                     %B点左侧seigema（MPa）、route、fai、de（mm）、裂缝宽度（mm）
        wBr(2) wBr(3) wBr(4) wBr(5) wBr(1) 0;                     %B点右侧seigema（MPa）、route、fai、de（mm）、裂缝宽度（mm）
        wBC(2) wBC(3) wBC(4) wBC(5) wBC(1) 0;                     %BC右侧seigema（MPa）、route、fai、de（mm）、裂缝宽度（mm）
        Money(1) Money(3) Money(5) Money(6) Money(7) 0];          %顶筋间距150时，顶筋费用、底筋费、砼费用、水平筋费用、总费用

elseif n==3
B=BB(n) ;       
hhhh=[250 250 250;300 300 300;350 350 350;400 400 400;
      400 250 250;400 300 300;400 350 350;350 300 300;
      350 250 250;300 250 250;400 400 250;400 400 300;
      400 400 350;350 350 250;350 350 300;300 300 250;
      400 350 300;400 350 250;400 300 250;350 300 250];
  for i=1:B
      if min([AAAA(i,1) AAAA(i,3) AAAA(i,5)])==0    %把AAAA中的有0的行找出来，并赋大值与该行方便后续找最经济墙厚
         AAAA(i,:)=[999999 999999 999999 999999 999999 999999];
      end
  end
  ss=AAAA(:,5);
  sss=find(ss==min(ss));%%%%%返回最优成本行号
  if max([AAAA(sss,1) AAAA(sss,3) AAAA(sss,5)])==999999 %若所有解配筋均存在0值，则表明内力超出程序上限或裂缝选筋超出程序上限，无法配筋计算和选筋
      disp('内力超出程序上限或裂缝选筋超出程序上限，计算失败'); 
      return
  end
  h=hhhh(sss,:);    %得到最优墙厚       
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%得到最优墙厚后，在运行一次计算程序，用于填写计算书
hunningtu=tongdengji(C);%混凝土标号
E=hunningtu(2);%砼弹性模量
fc=hunningtu(1);%砼抗压强度设计值
A=1000*h;%截面积
I=1000*h.^3/12;%截面惯性矩
fy=gangjindengji(F);%钢筋抗拉强度设计值
ft=hunningtu(4);%混凝土抗拉强度设计值
K=zonggangjuzhen(n,E,A,I,H);%得到总刚度矩阵
Q=hezaijisuan(n,H,r,hh,p0,rg,rq);%得到计算荷载
f01=dengxiaojiedianhezai01(n,H,Q);%得到墙底固结时等效节点荷载，已划行划列
f02=dengxiaojiedianhezai02(n,H,Q);%得到墙底铰结时等效节点荷载，已划行划列
K01=weiyibianjie01(n,K);%得到划行划列之后，墙底固结的总刚度矩阵
K02=weiyibianjie02(n,K);%得到划行划列之后，墙底铰结的总刚度矩阵
M01=neilijisuan01(E,A,I,K01,f01,n,Q,H);%得到墙底固结时，各节点内力
M02=neilijisuan02(E,A,I,K02,f02,n,Q,H);%得到墙底铰结时，各节点内力
MM=neilitiaofu(M01,M02,T);%进行内力调幅，得到配筋计算所需要个节点弯矩和剪力
Mmax=kuazhongzuidaM(MM,n,Q,H);%得到内力调幅后跨中最大弯矩
M=neilizuhe(n,MM,Mmax);%将负弯矩和正弯矩集成到一个列向量中，方便后续调用
As=peijinjisuan(M,cs,n,h,fy,fc,ft);%得到各节点和跨中的计算配筋值，满足最小配筋率
Ass=zuhezhengfujin(n,As,ft,fy,h,M,cs,C,rg);%得到间距150顶筋、间距200顶筋和底筋
Ashui=shuipingjin(n,h);%得到水平筋
Money=chengben(Ass,Ashui,n,Qh,Qg,h,H,cs,s,Z,C,F); %计算成本      
  
d=[Ass(1,1) fix(1000/Ass(1,2)) Ass(1,3) fix(1000/Ass(1,4))];  %重新根据实配筋计算A点裂缝，用于填计算书
wA=liefeng2(M(1),cs,d,Ass(1,5),h(1),C,rg); 
d=[Ass(9,1) fix(1000/Ass(9,2)) 0 0];                          %重新根据实配筋计算AB裂缝，用于填计算书
wAB=liefeng2(M(2),cs,d,Ass(9,5),h(1),C,rg);  
d=[Ass(2,1) fix(1000/Ass(2,2)) Ass(2,3) fix(1000/Ass(2,4))];  %重新根据实配筋计算B点左侧裂缝，用于填计算书
wBl=liefeng2(M(3),cs,d,Ass(2,5),h(1),C,rg); 
d=[Ass(2,1) fix(1000/Ass(2,2)) Ass(2,3) fix(1000/Ass(2,4))];  %重新根据实配筋计算B点右侧裂缝，用于填计算书
wBr=liefeng2(M(3),cs,d,Ass(2,5),h(2),C,rg); 
d=[Ass(10,1) fix(1000/Ass(10,2)) 0 0];                        %重新根据实配筋计算BC裂缝，用于填计算书
wBC=liefeng2(M(4),cs,d,Ass(10,5),h(2),C,rg);
d=[Ass(3,1) fix(1000/Ass(3,2)) Ass(3,3) fix(1000/Ass(3,4))];  %重新根据实配筋计算C点左侧裂缝，用于填计算书
wCl=liefeng2(M(5),cs,d,Ass(3,5),h(2),C,rg);   
d=[Ass(3,1) fix(1000/Ass(3,2)) Ass(3,3) fix(1000/Ass(3,4))];  %重新根据实配筋计算C点右侧裂缝，用于填计算书
wCr=liefeng2(M(5),cs,d,Ass(3,5),h(3),C,rg);   
d=[Ass(11,1) fix(1000/Ass(11,2)) 0 0];                        %重新根据实配筋计算CD裂缝，用于填计算书
wCD=liefeng2(M(6),cs,d,Ass(11,5),h(3),C,rg);  

%%%%%%%%%%%%%%%%%%%%%%%%%% 46x6 的矩阵，用于填写计算书，格式同计算书
target=[E 0 0 0 0 0;                                              %砼弹性模量，MPa
        fc 0 0 0 0 0;                                             %砼抗压强度，MPa
        hunningtu(3) 0 0 0 0 0;                                   %砼抗拉强度标准值，MPa
        fy 0 0 0 0 0;                                             %钢筋强度，MPa
        2.0*10^5 0 0 0 0 0;                                       %钢筋弹模，MPa
        h(1) h(2) h(3) 0 0 0;                                     %最优墙厚
        max(0.2/100,45*ft/fy) 0 0 0 0 0;                          %最小配筋率
        Q(1) Q(2) Q(3) Q(4) 0 0;                                  %A、B、C、D点的荷载设计值 ,kN/m
        M01(1,1)/10^6 M01(1,2)/10^6 M01(1,3)/10^6 M01(1,4)/10^6 M01(1,5)/10^6 0;   %墙底固结A点弯矩设计值、B点两侧弯矩设计值、C点两侧弯矩设计值，kN/m
        M01(2,1)/10^3 M01(2,2)/10^3 M01(2,3)/10^3 M01(2,4)/10^3 0 0;               %墙底固结A点支反力、B点支反力，C点支反力，D点支反力，kN       
        0             M02(1,2)/10^6 M02(1,3)/10^6 M02(1,4)/10^6 M02(1,5)/10^6 0;   %墙底铰结A点弯矩设计值、B点两侧弯矩设计值、C点两侧弯矩设计值，kN/m
        M02(2,1)/10^3 M02(2,2)/10^3 M02(2,3)/10^3 M02(2,4)/10^3 0 0;               %墙底铰结A点支反力、B点支反力，C点支反力，D点支反力，kN        
        MM(1,1)/10^6 MM(1,2)/10^6 MM(1,3)/10^6 MM(1,4)/10^6 MM(1,5)/10^6 0;        %调幅后A点弯矩设计值、B点两侧弯矩设计值、C点两侧弯矩设计值，kN/m
        MM(2,1)/10^3 MM(2,2)/10^3 MM(2,3)/10^3 MM(2,4)/10^3 0 0;                   %调幅后A点支反力、B点支反力，C点支反力，D点支反力，kN         
        M(2)/10^6 M(4)/10^6 M(6)/10^6 0 0 0;                      %AB、BC、CD跨中最大弯矩，kN·m
        h(1)-cs-16/2 h(2)-cs-16/2 h(3)-cs-16/2 0 0 0;             %配筋计算假定直径为16mm时的h0，mm
        0.8/(1+fy/(2.0*10^5*0.0033)) 0 0 0 0 0;                   %界限相对受压区高度
        As(3,8) As(2,8) As(4,8) 0 0 0;                            %CD跨中的受压区高度x(mm)、计算纵筋（mm2）、相对受压区高度
        As(3,7) As(2,7) As(4,7) 0 0 0;                            %C点右侧受压区高度x(mm)、计算纵筋（mm2）、相对受压区高度
        As(3,6) As(2,6) As(4,6) 0 0 0;                            %C点左侧受压区高度x(mm)、计算纵筋（mm2）、相对受压区高度
        As(3,5) As(2,5) As(4,5) 0 0 0;                            %BC跨中的受压区高度x(mm)、计算纵筋（mm2）、相对受压区高度
        As(3,4) As(2,4) As(4,4) 0 0 0;                            %B点右侧受压区高度x(mm)、计算纵筋（mm2）、相对受压区高度
        As(3,3) As(2,3) As(4,3) 0 0 0;                            %B点左侧受压区高度x(mm)、计算纵筋（mm2）、相对受压区高度
        As(3,2) As(2,2) As(4,2) 0 0 0;                            %AB跨中的受压区高度x(mm)、计算纵筋（mm2）、相对受压区高度
        As(3,1) As(2,1) As(4,1) 0 0 0;                            %A点的受压区高度x(mm)、计算纵筋（mm2）、相对受压区高度   
        As(1,1) Ass(1,1) Ass(1,2) Ass(1,3) Ass(1,4) Ass(1,5);     %A点计算面积(mm2)、实配通长直径(mm)、实配通长间距（mm）、实配附加直径(mm)、实配附加间距（mm）、实配面积（mm2)
        As(1,3) Ass(2,1) Ass(2,2) Ass(2,3) Ass(2,4) Ass(2,5);     %B点计算面积(mm2)、实配通长直径(mm)、实配通长间距（mm）、实配附加直径(mm)、实配附加间距（mm）、实配面积（mm2)
        As(1,5) Ass(3,1) Ass(3,2) Ass(3,3) Ass(3,4) Ass(3,5);     %C点计算面积(mm2)、实配通长直径(mm)、实配通长间距（mm）、实配附加直径(mm)、实配附加间距（mm）、实配面积（mm2)
        As(1,7) Ass(4,1) Ass(4,2) 0        0        Ass(4,5);     %D点计算面积(mm2)、实配通长直径(mm)、实配通长间距（mm）、实配面积（mm2)    
        As(1,2) Ass(9,1) Ass(9,2) 0        0        Ass(9,5);     %AB跨中计算面积(mm2)、实配直径(mm)、实配间距（mm）、实配面积（mm2)
        As(1,4) Ass(10,1) Ass(10,2) 0      0       Ass(10,5);     %BC跨中计算面积(mm2)、实配直径(mm)、实配间距（mm）、实配面积（mm2)
        As(1,6) Ass(11,1) Ass(11,2) 0      0       Ass(11,5);     %CD跨中计算面积(mm2)、实配直径(mm)、实配间距（mm）、实配面积（mm2)
        Ashui(1,1) Ashui(1,2) 0 0 Ashui(1,5) 0;                   %一层挡墙的水平筋单侧直径（mm）、间距（mm）、面积（mm2）
        Ashui(2,1) Ashui(2,2) 0 0 Ashui(2,5) 0;                   %二层挡墙的水平筋单侧直径（mm）、间距（mm）、面积（mm2）
        Ashui(3,1) Ashui(3,2) 0 0 Ashui(3,5) 0;                   %三层挡墙的水平筋单侧直径（mm）、间距（mm）、面积（mm2） 
        MM(1)/10^6/rg MM(2)/10^6/rg MM(3)/10^6/rg MM(4)/10^6/rg MM(5)/10^6/rg 0;   %A点准永久弯矩、B点两侧准永久弯矩、C点两侧准永久弯矩，kN·m
        M(2)/10^6/rg M(4)/10^6/rg M(6)/10^6/rg 0 0 0 ;            %AB、BC、CD准永久弯矩准永久弯矩，kN·m
        wA(2) wA(3) wA(4) wA(5) wA(1) 0;                          %A点seigema（MPa）、route、fai、de（mm）、裂缝宽度（mm）
        wAB(2) wAB(3) wAB(4) wAB(5) wAB(1) 0;                     %AB跨中seigema（MPa）、route、fai、de（mm）、裂缝宽度（mm）
        wBl(2) wBl(3) wBl(4) wBl(5) wBl(1) 0;                     %B点左侧seigema（MPa）、route、fai、de（mm）、裂缝宽度（mm）
        wBr(2) wBr(3) wBr(4) wBr(5) wBr(1) 0;                     %B点右侧seigema（MPa）、route、fai、de（mm）、裂缝宽度（mm）
        wBC(2) wBC(3) wBC(4) wBC(5) wBC(1) 0;                     %BC右侧seigema（MPa）、route、fai、de（mm）、裂缝宽度（mm）
        wCl(2) wCl(3) wCl(4) wCl(5) wCl(1) 0;                     %C点左侧seigema（MPa）、route、fai、de（mm）、裂缝宽度（mm）
        wCr(2) wCr(3) wCr(4) wCr(5) wCr(1) 0;                     %C点右侧seigema（MPa）、route、fai、de（mm）、裂缝宽度（mm）  
        wCD(2) wCD(3) wCD(4) wCD(5) wCD(1) 0;                     %BC右侧seigema（MPa）、route、fai、de（mm）、裂缝宽度（mm）
        Money(1) Money(3) Money(5) Money(6) Money(7) 0];          %顶筋间距150时，顶筋费用、底筋费、砼费用、水平筋费用、总费用  

elseif n==4
B=BB(n) ;            
hhhh=[250 250 250 250;300 300 300 300;350 350 350 350;400 400 400 400;
      400 400 400 250;400 400 400 300;400 400 400 350;350 350 350 250;
      350 350 350 300;300 300 300 250;
      400 250 250 250;400 300 300 300;400 350 350 350;350 250 250 250;
      350 300 300 300;300 250 250 250;
      400 400 300 250;400 400 350 250;400 400 350 300;350 350 300 250;
      400 300 300 250;400 350 350 250;400 350 350 300;350 300 300 250;
      400 300 250 250;400 350 250 250;400 350 300 300;350 300 250 250;
      400 400 250 250;400 400 300 300;400 400 350 350;350 350 250 250;
      350 350 300 300;300 300 250 250;
      400 350 300 250];
  for i=1:B
      if min([AAAA(i,1) AAAA(i,3) AAAA(i,5)])==0    %把AAAA中的有0的行找出来，并赋大值与该行方便后续找最经济墙厚
         AAAA(i,:)=[999999 999999 999999 999999 999999 999999];
      end
  end
  ss=AAAA(:,5);
  sss=find(ss==min(ss));%%%%%返回最优成本行号
  if max([AAAA(sss,1) AAAA(sss,3) AAAA(sss,5)])==999999 %若所有解配筋均存在0值，则表明内力超出程序上限或裂缝选筋超出程序上限，无法配筋计算和选筋
      disp('内力超出程序上限或裂缝选筋超出程序上限，计算失败'); 
      return
  end
  h=hhhh(sss,:);    %得到最优墙厚   
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%得到最优墙厚后，在运行一次计算程序，用于填写计算书
hunningtu=tongdengji(C);%混凝土标号
E=hunningtu(2);%砼弹性模量
fc=hunningtu(1);%砼抗压强度设计值
A=1000*h;%截面积
I=1000*h.^3/12;%截面惯性矩
fy=gangjindengji(F);%钢筋抗拉强度设计值
ft=hunningtu(4);%混凝土抗拉强度设计值
K=zonggangjuzhen(n,E,A,I,H);%得到总刚度矩阵
Q=hezaijisuan(n,H,r,hh,p0,rg,rq);%得到计算荷载
f01=dengxiaojiedianhezai01(n,H,Q);%得到墙底固结时等效节点荷载，已划行划列
f02=dengxiaojiedianhezai02(n,H,Q);%得到墙底铰结时等效节点荷载，已划行划列
K01=weiyibianjie01(n,K);%得到划行划列之后，墙底固结的总刚度矩阵
K02=weiyibianjie02(n,K);%得到划行划列之后，墙底铰结的总刚度矩阵
M01=neilijisuan01(E,A,I,K01,f01,n,Q,H);%得到墙底固结时，各节点内力
M02=neilijisuan02(E,A,I,K02,f02,n,Q,H);%得到墙底铰结时，各节点内力
MM=neilitiaofu(M01,M02,T);%进行内力调幅，得到配筋计算所需要个节点弯矩和剪力
Mmax=kuazhongzuidaM(MM,n,Q,H);%得到内力调幅后跨中最大弯矩
M=neilizuhe(n,MM,Mmax);%将负弯矩和正弯矩集成到一个列向量中，方便后续调用
As=peijinjisuan(M,cs,n,h,fy,fc,ft);%得到各节点和跨中的计算配筋值，满足最小配筋率
Ass=zuhezhengfujin(n,As,ft,fy,h,M,cs,C,rg);%得到间距150顶筋、间距200顶筋和底筋
Ashui=shuipingjin(n,h);%得到水平筋
Money=chengben(Ass,Ashui,n,Qh,Qg,h,H,cs,s,Z,C,F); %计算成本      
 
d=[Ass(1,1) fix(1000/Ass(1,2)) Ass(1,3) fix(1000/Ass(1,4))];  %重新根据实配筋计算A点裂缝，用于填计算书
wA=liefeng2(M(1),cs,d,Ass(1,5),h(1),C,rg); 
d=[Ass(11,1) fix(1000/Ass(11,2)) 0 0];                        %重新根据实配筋计算AB裂缝，用于填计算书
wAB=liefeng2(M(2),cs,d,Ass(11,5),h(1),C,rg);  
d=[Ass(2,1) fix(1000/Ass(2,2)) Ass(2,3) fix(1000/Ass(2,4))];  %重新根据实配筋计算B点左侧裂缝，用于填计算书
wBl=liefeng2(M(3),cs,d,Ass(2,5),h(1),C,rg); 
d=[Ass(2,1) fix(1000/Ass(2,2)) Ass(2,3) fix(1000/Ass(2,4))];  %重新根据实配筋计算B点右侧裂缝，用于填计算书
wBr=liefeng2(M(3),cs,d,Ass(2,5),h(2),C,rg); 
d=[Ass(12,1) fix(1000/Ass(12,2)) 0 0];                        %重新根据实配筋计算BC裂缝，用于填计算书
wBC=liefeng2(M(4),cs,d,Ass(12,5),h(2),C,rg);
d=[Ass(3,1) fix(1000/Ass(3,2)) Ass(3,3) fix(1000/Ass(3,4))];  %重新根据实配筋计算C点左侧裂缝，用于填计算书
wCl=liefeng2(M(5),cs,d,Ass(3,5),h(2),C,rg);   
d=[Ass(3,1) fix(1000/Ass(3,2)) Ass(3,3) fix(1000/Ass(3,4))];  %重新根据实配筋计算C点右侧裂缝，用于填计算书
wCr=liefeng2(M(5),cs,d,Ass(3,5),h(3),C,rg);   
d=[Ass(13,1) fix(1000/Ass(13,2)) 0 0];                        %重新根据实配筋计算CD裂缝，用于填计算书
wCD=liefeng2(M(6),cs,d,Ass(13,5),h(3),C,rg);   
d=[Ass(4,1) fix(1000/Ass(4,2)) Ass(4,3) fix(1000/Ass(4,4))];  %重新根据实配筋计算D点左侧裂缝，用于填计算书
wDl=liefeng2(M(7),cs,d,Ass(4,5),h(3),C,rg);   
d=[Ass(4,1) fix(1000/Ass(4,2)) Ass(4,3) fix(1000/Ass(4,4))];  %重新根据实配筋计算D点右侧裂缝，用于填计算书
wDr=liefeng2(M(7),cs,d,Ass(4,5),h(4),C,rg); 
d=[Ass(14,1) fix(1000/Ass(14,2)) 0 0];                        %重新根据实配筋计算DE裂缝，用于填计算书
wDE=liefeng2(M(8),cs,d,Ass(14,5),h(4),C,rg); 

%%%%%%%%%%%%%%%%%%%%%%%%%% 55x7 的矩阵，用于填写计算书，格式同计算书
target=[E 0 0 0 0 0 0;                                              %砼弹性模量，MPa
        fc 0 0 0 0 0 0;                                             %砼抗压强度，MPa
        hunningtu(3) 0 0 0 0 0 0;                                   %砼抗拉强度标准值，MPa
        fy 0 0 0 0 0 0;                                             %钢筋强度，MPa
        2.0*10^5 0 0 0 0 0 0;                                       %钢筋弹模，MPa
        h(1) h(2) h(3) h(4) 0 0 0;                                  %最优墙厚
        max(0.2/100,45*ft/fy) 0 0 0 0 0 0;                          %最小配筋率
        Q(1) Q(2) Q(3) Q(4) Q(5) 0 0;                               %A、B、C、D、E点的荷载设计值 ,kN/m
        M01(1,1)/10^6 M01(1,2)/10^6 M01(1,3)/10^6 M01(1,4)/10^6 M01(1,5)/10^6 M01(1,6)/10^6 M01(1,7)/10^6;   %墙底固结A点弯矩设计值、B点两侧弯矩设计值、C点两侧弯矩设计值、D点两侧弯矩设计值，kN/m
        M01(2,1)/10^3 M01(2,2)/10^3 M01(2,3)/10^3 M01(2,4)/10^3 M01(2,5)/10^3 0 0;                           %墙底固结A点支反力、B点支反力，C点支反力，D点支反力，E点支反力 kN       
        0             M02(1,2)/10^6 M02(1,3)/10^6 M02(1,4)/10^6 M02(1,5)/10^6 M02(1,6)/10^6 M02(1,7)/10^6;   %墙底铰结A点弯矩设计值、B点两侧弯矩设计值、C点两侧弯矩设计值、D点两侧弯矩设计值，kN/m
        M02(2,1)/10^3 M02(2,2)/10^3 M02(2,3)/10^3 M02(2,4)/10^3 M02(2,5)/10^3 0 0;                           %墙底铰结A点支反力、B点支反力，C点支反力，D点支反力，E点支反力，kN        
        MM(1,1)/10^6 MM(1,2)/10^6 MM(1,3)/10^6 MM(1,4)/10^6 MM(1,5)/10^6 MM(1,6)/10^6 MM(1,7)/10^6;          %调幅后A点弯矩设计值、B点两侧弯矩设计值、C点两侧弯矩设计值、D点两侧弯矩设计值，kN/m
        MM(2,1)/10^3 MM(2,2)/10^3 MM(2,3)/10^3 MM(2,4)/10^3 MM(2,5)/10^3 0 0;                                %调幅后A点支反力、B点支反力，C点支反力，D点支反力，E点支反力，kN         
        M(2)/10^6 M(4)/10^6 M(6)/10^6 M(8)/10^6 0 0 0;              %AB、BC、CD、DE跨中最大弯矩，kN·m
        h(1)-cs-16/2 h(2)-cs-16/2 h(3)-cs-16/2 h(4)-cs-16/2 0 0 0;  %配筋计算假定直径为16mm时的h0，mm
        0.8/(1+fy/(2.0*10^5*0.0033)) 0 0 0 0 0 0;                   %界限相对受压区高度
        As(3,11) As(2,11) As(4,11) 0 0 0 0;                         %DE跨中的受压区高度x(mm)、计算纵筋（mm2）、相对受压区高度
        As(3,10) As(2,10) As(4,10) 0 0 0 0;                         %D点右侧受压区高度x(mm)、计算纵筋（mm2）、相对受压区高度
        As(3,9)  As(2,9)  As(4,9)  0 0 0 0;                         %D点左侧受压区高度x(mm)、计算纵筋（mm2）、相对受压区高度  
        As(3,8)  As(2,8)  As(4,8)  0 0 0 0;                         %CD跨中的受压区高度x(mm)、计算纵筋（mm2）、相对受压区高度
        As(3,7)  As(2,7)  As(4,7)  0 0 0 0;                         %C点右侧受压区高度x(mm)、计算纵筋（mm2）、相对受压区高度
        As(3,6)  As(2,6)  As(4,6)  0 0 0 0;                         %C点左侧受压区高度x(mm)、计算纵筋（mm2）、相对受压区高度
        As(3,5)  As(2,5)  As(4,5)  0 0 0 0;                         %BC跨中的受压区高度x(mm)、计算纵筋（mm2）、相对受压区高度
        As(3,4)  As(2,4)  As(4,4)  0 0 0 0;                         %B点右侧受压区高度x(mm)、计算纵筋（mm2）、相对受压区高度
        As(3,3)  As(2,3)  As(4,3)  0 0 0 0;                         %B点左侧受压区高度x(mm)、计算纵筋（mm2）、相对受压区高度
        As(3,2)  As(2,2)  As(4,2)  0 0 0 0;                         %AB跨中的受压区高度x(mm)、计算纵筋（mm2）、相对受压区高度
        As(3,1)  As(2,1)  As(4,1)  0 0 0 0;                         %A点的受压区高度x(mm)、计算纵筋（mm2）、相对受压区高度   
        As(1,1) Ass(1,1) Ass(1,2) Ass(1,3) Ass(1,4) Ass(1,5) 0;     %A点计算面积(mm2)、实配通长直径(mm)、实通长加间距（mm）、实配附加直径(mm)、实配附加间距（mm）、实配面积（mm2)
        As(1,3) Ass(2,1) Ass(2,2) Ass(2,3) Ass(2,4) Ass(2,5) 0;     %B点计算面积(mm2)、实配通长直径(mm)、实通长加间距（mm）、实配附加直径(mm)、实配附加间距（mm）、实配面积（mm2)
        As(1,5) Ass(3,1) Ass(3,2) Ass(3,3) Ass(3,4) Ass(3,5) 0;     %C点计算面积(mm2)、实配通长直径(mm)、实通长加间距（mm）、实配附加直径(mm)、实配附加间距（mm）、实配面积（mm2)
        As(1,7) Ass(4,1) Ass(4,2) Ass(4,3) Ass(4,4) Ass(4,5) 0;     %D点计算面积(mm2)、实配通长直径(mm)、实通长加间距（mm）、实配附加直径(mm)、实配附加间距（mm）、实配面积（mm2)
        As(1,9) Ass(5,1) Ass(5,2) 0        0        Ass(5,5) 0;     %E点计算面积(mm2)、实配通长直径(mm)、实配通长间距（mm）、实配面积（mm2)
        As(1,2) Ass(11,1) Ass(11,2) 0      0       Ass(11,5) 0;     %AB跨中计算面积(mm2)、实配直径(mm)、实配间距（mm）、实配面积（mm2)
        As(1,4) Ass(12,1) Ass(12,2) 0      0       Ass(12,5) 0;     %BC跨中计算面积(mm2)、实配直径(mm)、实配间距（mm）、实配面积（mm2)
        As(1,6) Ass(13,1) Ass(13,2) 0      0       Ass(13,5) 0;     %CD跨中计算面积(mm2)、实配直径(mm)、实配间距（mm）、实配面积（mm2)
        As(1,8) Ass(14,1) Ass(14,2) 0      0       Ass(14,5) 0;     %DE跨中计算面积(mm2)、实配直径(mm)、实配间距（mm）、实配面积（mm2) 
        Ashui(1,1) Ashui(1,2) 0 0 Ashui(1,5) 0 0;                   %一层挡墙的水平筋单侧直径（mm）、间距（mm）、面积（mm2）
        Ashui(2,1) Ashui(2,2) 0 0 Ashui(2,5) 0 0;                   %二层挡墙的水平筋单侧直径（mm）、间距（mm）、面积（mm2）
        Ashui(3,1) Ashui(3,2) 0 0 Ashui(3,5) 0 0;                   %三层挡墙的水平筋单侧直径（mm）、间距（mm）、面积（mm2）
        Ashui(4,1) Ashui(4,2) 0 0 Ashui(4,5) 0 0;                   %四层挡墙的水平筋单侧直径（mm）、间距（mm）、面积（mm2）       
        MM(1)/10^6/rg MM(2)/10^6/rg MM(3)/10^6/rg MM(4)/10^6/rg MM(5)/10^6/rg MM(6)/10^6/rg MM(7)/10^6/rg;  %A点准永久弯矩、B点两侧准永久弯矩、C点两侧准永久弯矩、D点两侧准永久弯矩，kN·m
        M(2)/10^6/rg M(4)/10^6/rg M(6)/10^6/rg M(8)/10^6/rg 0 0 0;  %AB、BC、CD、DE准永久弯矩，kN·m
        wA(2)  wA(3)  wA(4)  wA(5)  wA(1) 0 0;                      %A点seigema（MPa）、route、fai、de（mm）、裂缝宽度（mm）
        wAB(2) wAB(3) wAB(4) wAB(5) wAB(1) 0 0;                     %AB跨中seigema（MPa）、route、fai、de（mm）、裂缝宽度（mm）
        wBl(2) wBl(3) wBl(4) wBl(5) wBl(1) 0 0;                     %B点左侧seigema（MPa）、route、fai、de（mm）、裂缝宽度（mm）
        wBr(2) wBr(3) wBr(4) wBr(5) wBr(1) 0 0;                     %B点右侧seigema（MPa）、route、fai、de（mm）、裂缝宽度（mm）
        wBC(2) wBC(3) wBC(4) wBC(5) wBC(1) 0 0;                     %BC跨中seigema（MPa）、route、fai、de（mm）、裂缝宽度（mm）
        wCl(2) wCl(3) wCl(4) wCl(5) wCl(1) 0 0;                     %C点左侧seigema（MPa）、route、fai、de（mm）、裂缝宽度（mm）
        wCr(2) wCr(3) wCr(4) wCr(5) wCr(1) 0 0;                     %C点右侧seigema（MPa）、route、fai、de（mm）、裂缝宽度（mm）  
        wCD(2) wCD(3) wCD(4) wCD(5) wCD(1) 0 0;                     %CD跨中seigema（MPa）、route、fai、de（mm）、裂缝宽度（mm）
        wDl(2) wDl(3) wDl(4) wDl(5) wDl(1) 0 0;                     %D点左侧seigema（MPa）、route、fai、de（mm）、裂缝宽度（mm）
        wDr(2) wDr(3) wDr(4) wDr(5) wDr(1) 0 0;                     %D点右侧seigema（MPa）、route、fai、de（mm）、裂缝宽度（mm）
        wDE(2) wDE(3) wDE(4) wDE(5) wDE(1) 0 0;                     %DE跨中seigema（MPa）、route、fai、de（mm）、裂缝宽度（mm）
        Money(1) Money(3) Money(5) Money(6) Money(7) 0 0];          %顶筋间距150时，顶筋费用、底筋费、砼费用、水平筋费用、总费用 
            
end
end

