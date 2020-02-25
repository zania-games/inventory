// Fill out your copyright notice in the Description page of Project Settings.

#include "Griddable.h"

int32 IGriddable::NumRows() const
{
    return 1;
}

int32 IGriddable::NumColumns() const
{
    return 1;
}

void IGriddable::OnInsert() {}
void IGriddable::OnRemove() {}
void IGriddable::OnDrop() {}
