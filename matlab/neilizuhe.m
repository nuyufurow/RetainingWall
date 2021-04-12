function [ MM ] = neilizuhe(n,Mg,Mmaxg)
%���Ӻ������ڽ�neilijisuan�Ӻ�����kuazhongzuidaM�Ӻ�����ϳ�1������
% MgΪneilitiaofu(M01,M02,T)�õ��Ľڵ���غͼ���
% MmaxgΪkuazhongzuidaM�õ��Ŀ���������

if n==1
    MMg=[Mg(1,1) Mmaxg(1) Mg(1,2)];
    MM=MMg;
elseif n==2
    MMg=[Mg(1,1) Mmaxg(1) Mg(1,2) Mmaxg(2) Mg(1,4)];
    MM=MMg;   
elseif n==3
    MMg=[Mg(1,1) Mmaxg(1) Mg(1,2) Mmaxg(2) Mg(1,4) Mmaxg(3) Mg(1,6)];
    MM=MMg;
elseif n==4
    MMg=[Mg(1,1) Mmaxg(1) Mg(1,2) Mmaxg(2) Mg(1,4) Mmaxg(3) Mg(1,6) Mmaxg(4) Mg(1,8)];
    MM=MMg;
end

end

