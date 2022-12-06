#include<iostream>
using namespace std;

struct Node {
    int val;
    Node *childLeft; 
    Node *childRight;
};

// Head
class FibTree {
public:
    FibTree(int n);            
    ~FibTree(void);     
    Node* fibonacciCall(int n);
    void preorder(Node* node);     
    int size(Node* node);
    int depth(Node* node);
    int leaves(Node* node);
    void display(void);
    
private:
    Node *root; // Pointer to the root node
};

FibTree::FibTree(int n){
    this->root = fibonacciCall(n);

}

FibTree::~FibTree(){
    Node* node = new Node;
    while (node->childLeft != NULL && node->childRight != NULL)
    {
        delete node->childLeft;
        delete node->childRight;
    }
    delete node;
    delete root;
    this->root = NULL;
}

Node* insertNode(int n, Node* childLeft, Node* childRight){
    Node* node = new Node;
    node->val = n;
    node->childLeft = childLeft;
    node->childRight = childRight;
    return node;
    delete node;
}

Node* FibTree::fibonacciCall(int n){
    Node* node = new Node;
    if(n <= 1){
        return insertNode(1, NULL, NULL);
    } else{
        node->childLeft = fibonacciCall(n - 1);
        node->childRight = fibonacciCall(n - 2);
        int sum = node->val = node->childLeft->val + node->childRight->val;
        return insertNode(sum, node->childLeft, node->childRight);
    }
}

void FibTree::preorder(Node* node){
    if(node == NULL){
        return;
    }
    cout<< node->val << " ";
    preorder(node->childLeft);
    preorder(node->childRight);
}

int FibTree::size(Node* node){
    if(node == NULL){
        return 0;
    } else {
        return(size(node->childLeft) + size(node->childRight) + 1); 
    }
}

int FibTree::depth(Node* node){
    if(node == NULL){
        return 0;
    } else {
        int debthchildLeft = depth(node->childLeft);
        int debthchildRight = depth(node->childRight);
        int sum = (debthchildLeft > debthchildRight) ? debthchildLeft + 1 : debthchildRight + 1; 
        return sum;
    }
}

int FibTree::leaves(Node* node){
    if(node == NULL){
        return 0;
    } 
    int sum = ((node->childLeft == NULL) && (node->childRight == NULL)) ? 1 : (leaves(node->childLeft) + leaves(node->childRight));
    return sum;
}

void FibTree::display(){
    cout<< "Call tree in pre-order: ";
    preorder(this->root);
    cout<< endl;
    cout<< "Call tree size: " << size(this->root) << endl;
    cout<< "Call tree depth: " << depth(this->root) << endl;
    cout<< "Call tree leafs: " << leaves(this->root) << endl;
}


int main(){

    int n;
    cin>> n;
    FibTree t(n);
    t.display();
    return 0;
}