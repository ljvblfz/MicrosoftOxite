//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Infrastructure
{
    public interface IBackgroundServiceExecutor
    {
        BackgroundServiceExecutorStatus Status { get; }
        void Start();
        void Pause();
        void Stop();
    }
}