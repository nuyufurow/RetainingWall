function [ K ] = zonggangjuzhen( n,E,A,I,H )
%����ܸնȾ���
%n������ǽ����
%H����Ϊÿ�㵲ǽ���
 K=zeros((n+1)*3,(n+1)*3);                     %�����ʼ��ȫ0����,һ���ڵ���������
if n==1
 k1=PlaneFrameElementStiffness(E(1),A(1),I(1),H(1,1));  %Ϊ��Ԫ�նȾ���ͬ�ܸնȾ���
 K=k1;                                         %����ͬ�ܸ�
elseif n==2
 k1=PlaneFrameElementStiffness(E(1),A(1),I(1),H(1,1));  %һ�㵥Ԫ�նȾ���
 k2=PlaneFrameElementStiffness(E(1),A(2),I(2),H(1,2));  %���㵥Ԫ�նȾ���
 K=PlaneFrameAssemble(K,k1,1,2);               %��k1��װ��K
 K=PlaneFrameAssemble(K,k2,2,3);               %��k2��װ��K
elseif n==3
 k1=PlaneFrameElementStiffness(E(1),A(1),I(1),H(1,1));  %һ�㵥Ԫ�նȾ���
 k2=PlaneFrameElementStiffness(E(1),A(2),I(2),H(1,2));  %���㵥Ԫ�նȾ���
 k3=PlaneFrameElementStiffness(E(1),A(3),I(3),H(1,3));  %���㵥Ԫ�նȾ���
 K=PlaneFrameAssemble(K,k1,1,2);               %��k1��װ��K
 K=PlaneFrameAssemble(K,k2,2,3);               %��k2��װ��K
 K=PlaneFrameAssemble(K,k3,3,4);               %��k3��װ��K
elseif n==4
 k1=PlaneFrameElementStiffness(E(1),A(1),I(1),H(1,1));  %һ�㵥Ԫ�նȾ���
 k2=PlaneFrameElementStiffness(E(1),A(2),I(2),H(1,2));  %���㵥Ԫ�նȾ���
 k3=PlaneFrameElementStiffness(E(1),A(3),I(3),H(1,3));  %���㵥Ԫ�նȾ���
 k4=PlaneFrameElementStiffness(E(1),A(4),I(4),H(1,4));  %�Ĳ㵥Ԫ�նȾ���
 K=PlaneFrameAssemble(K,k1,1,2);               %��k1��װ��K
 K=PlaneFrameAssemble(K,k2,2,3);               %��k2��װ��K
 K=PlaneFrameAssemble(K,k3,3,4);               %��k3��װ��K
 K=PlaneFrameAssemble(K,k4,4,5);               %��k4��װ��K
end
end

