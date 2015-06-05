/*************************************************
 * Copyright (C) 2007 Google Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*************************************************/

using System;
using System.Collections;

namespace GCheckout.NotificationQueue {
	public class NotificationFileCollection {
    private ArrayList _NFiles;

		public NotificationFileCollection() {
			_NFiles = new ArrayList();
		}

    public void Add(NotificationFile NFile) {
      _NFiles.Add(NFile);
    }

    public NotificationFile GetFileAt(int Index) {
      return (NotificationFile) _NFiles[Index];
    }

    public void RemoveFileAt(int Index) {
      _NFiles.RemoveAt(Index);
    }

    public void RemoveNewFiles(int MinAgeSeconds) {
      for (int i = _NFiles.Count - 1; i >= 0; i--) {
        NotificationFile NFile = (NotificationFile) _NFiles[i];
        if (NFile.Age.TotalSeconds < MinAgeSeconds) {
          _NFiles.RemoveAt(i);
        }
      }
    }

    public void SortByAge() {
      _NFiles.Sort(new NotificationFileComparer());
    }

    public int Count {
      get {
        return _NFiles.Count;
      }
    }

	}
}
