function [ K ] = weiyibianjie02( n,K )
%���Ӻ���Ϊ��ǽ�½�ʱ��λ�Ʊ߽�����,��������л���֮����ܸնȾ���
%�����KΪ�ܸնȾ���nΪ��ǽ����
if n==1 
   K=K([3,4,6],[3,4,6]);% ���û��л��У��ų�1�ڵ�2��λ�ƣ��ų�2�ڵ�yλ��
elseif n==2 
   K=K([3,4,6,7,9],[3,4,6,7,9]);% ���û��л��У��ų�1�ڵ�2��λ�ƣ��ų�2��3�ڵ�yλ��
elseif n==3 
   K=K([3,4,6,7,9,10,12],[3,4,6,7,9,10,12]);% ���û��л��У��ų�1�ڵ�2��λ�ƣ��ų�2��3��4�ڵ�yλ��
 elseif n==4 
   K=K([3,4,6,7,9,10,12,13,15],[3,4,6,7,9,10,12,13,15]);% ���û��л��У��ų�1�ڵ�2��λ�ƣ��ų�2��3��4��5�ڵ�yλ��  

end
