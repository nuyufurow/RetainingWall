function [ AAA ] = hezi( n,C,h,H,r,hh,p0,rg,rq,F,Qh,Qg,Z,s,cs,T )
% 本子函数用于输出指定h、H等条件时的配筋成本
% n 为层数
% C 混凝土等级
% H 层高
% r 填土容重N/mm3
% hh 墙顶覆土厚度
% p0 活荷载N/mm2
% rg 恒荷载分项系数
% rq 活荷载分项系数
% F 钢筋等级为HRB400
% Qh 每方混凝土的单价
% Qg 每吨钢筋的单价
% Z 挡墙抗震等级
% s 地下室顶板厚度
% cs 保护层厚度
% T 内力调幅系数
hunningtu=tongdengji(C);%用CXX混凝土
E=hunningtu(2);%CXX弹性模量
fc=hunningtu(1);%CXX抗压强度设计值
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
Money=chengben(Ass,Ashui,n,Qh,Qg,h,H,cs,s,Z,C,F);%计算成本   
AAA=[Money(1) Money(2) Money(3) Money(4) Money(7) Money(8)];%储存150顶筋费用 200顶筋费用 150底筋费用 200底筋费用 间距150总费用 间距200总费用
    
end

