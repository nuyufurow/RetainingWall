function [ AAA ] = hezi( n,C,h,H,r,hh,p0,rg,rq,F,Qh,Qg,Z,s,cs,T )
% ���Ӻ����������ָ��h��H������ʱ�����ɱ�
% n Ϊ����
% C �������ȼ�
% H ���
% r ��������N/mm3
% hh ǽ���������
% p0 �����N/mm2
% rg ����ط���ϵ��
% rq ����ط���ϵ��
% F �ֽ�ȼ�ΪHRB400
% Qh ÿ���������ĵ���
% Qg ÿ�ָֽ�ĵ���
% Z ��ǽ����ȼ�
% s �����Ҷ�����
% cs ��������
% T ��������ϵ��
hunningtu=tongdengji(C);%��CXX������
E=hunningtu(2);%CXX����ģ��
fc=hunningtu(1);%CXX��ѹǿ�����ֵ
A=1000*h;%�����
I=1000*h.^3/12;%������Ծ�
fy=gangjindengji(F);%�ֽ��ǿ�����ֵ
ft=hunningtu(4);%����������ǿ�����ֵ
K=zonggangjuzhen(n,E,A,I,H);%�õ��ܸնȾ���
Q=hezaijisuan(n,H,r,hh,p0,rg,rq);%�õ��������
f01=dengxiaojiedianhezai01(n,H,Q);%�õ�ǽ�׹̽�ʱ��Ч�ڵ���أ��ѻ��л���
f02=dengxiaojiedianhezai02(n,H,Q);%�õ�ǽ�׽½�ʱ��Ч�ڵ���أ��ѻ��л���
K01=weiyibianjie01(n,K);%�õ����л���֮��ǽ�׹̽���ܸնȾ���
K02=weiyibianjie02(n,K);%�õ����л���֮��ǽ�׽½���ܸնȾ���
M01=neilijisuan01(E,A,I,K01,f01,n,Q,H);%�õ�ǽ�׹̽�ʱ�����ڵ�����
M02=neilijisuan02(E,A,I,K02,f02,n,Q,H);%�õ�ǽ�׽½�ʱ�����ڵ�����
MM=neilitiaofu(M01,M02,T);%���������������õ�����������Ҫ���ڵ���غͼ���
Mmax=kuazhongzuidaM(MM,n,Q,H);%�õ��������������������
M=neilizuhe(n,MM,Mmax);%������غ�����ؼ��ɵ�һ���������У������������
As=peijinjisuan(M,cs,n,h,fy,fc,ft);%�õ����ڵ�Ϳ��еļ������ֵ��������С�����
Ass=zuhezhengfujin(n,As,ft,fy,h,M,cs,C,rg);%�õ����150������200����͵׽�
Ashui=shuipingjin(n,h);%�õ�ˮƽ��
Money=chengben(Ass,Ashui,n,Qh,Qg,h,H,cs,s,Z,C,F);%����ɱ�   
AAA=[Money(1) Money(2) Money(3) Money(4) Money(7) Money(8)];%����150������� 200������� 150�׽���� 200�׽���� ���150�ܷ��� ���200�ܷ���
    
end

